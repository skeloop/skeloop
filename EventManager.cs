using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FMC
{

    public class EventManager
    {
        // => Hier sind alle Globalen Events
        public delegate void OnProjectCreate(); // Wird ausgelöst wenn ein neues Projekt erstellt wird.
        public delegate void OnRawModObjectCreate(ModObject modObject); // Wird ausgelöst wenn ein neues ModObject erstellt wird. RawModObject = Befindet sich noch im buffer.
        public delegate void OnModObjectCreate(ModObject modObject); // Wird ausgelöst wenn das RawModObject zum Projekt hinzugefügt wird

        public event OnProjectCreate SimpleEvent;

        protected virtual void TriggerEvent(string eventName)
        {
            var events = GetDelegateNames();
            foreach (var e in events)
            {
                if (e == eventName)
                {
                    Console.WriteLine("Event: "+eventName);
                }
            }
        }

        public string[] GetDelegateNames()
        {
            // Type-Informationen der aktuellen Klasse erhalten
            Type type = GetType();

            // Felder und Ereignisse durchsuchen
            var delegateNames = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(field => typeof(Delegate).IsAssignableFrom(field.FieldType))
                .Select(field => field.Name)
                .Union(
                    type.GetEvents(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(e => typeof(Delegate).IsAssignableFrom(e.EventHandlerType))
                    .Select(e => e.Name)
                )
                .ToArray();

            return delegateNames;
        }

        public void InvokeEventByName(string eventName)
        {
            // Type-Informationen der aktuellen Klasse erhalten
            Type type = GetType();

            // Event-Info anhand des Namens erhalten
            EventInfo eventInfo = type.GetEvent(eventName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (eventInfo != null)
            {
                // Feld-Info des Events erhalten (zur Zugriff auf den Delegate)
                FieldInfo fieldInfo = type.GetField(eventInfo.Name, BindingFlags.Instance | BindingFlags.NonPublic);

                if (fieldInfo != null)
                {
                    // Delegate-Instanz des Events erhalten
                    Delegate eventDelegate = (Delegate)fieldInfo.GetValue(this);

                    if (eventDelegate != null)
                    {
                        // Alle Abonnenten des Events aufrufen
                        foreach (var handler in eventDelegate.GetInvocationList())
                        {
                            handler.DynamicInvoke();
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine($"Event '{eventName}' nicht gefunden.");
            }
        }
    }
}

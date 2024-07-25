using FactorioConverter;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMC
{
    public class ModObject
    {
        public ModObjectType type;
        public List<PropertyInfo> properties = new();

        public bool saved = false;
        public int id = -1;
        public object GetPropertyValue(string property)
        {
            foreach (var prop in properties)
            {
                if (prop.type == property)
                {
                    return prop.value.ToString();
                }
            }
            return string.Empty;
        }

        public void SetPropertyValue(string property, object value)
        {
            foreach (var prop in properties)
            {
                if (prop.type == property)
                {
                    Console.WriteLine("set "+prop.type.ToString());
                    Console.WriteLine("from " + value);
                    prop.value = value;
                    Console.WriteLine("to"+ prop.value);
                    return;
                }
            }
        }

        public void DisplayPropertiesInformation()
        {
            Console.WriteLine($"ModObject '{GetPropertyValue("name") as string}' hat folgende Properties:"); // Jedes ModObject hat eine property 'name'.
            foreach(var prop in properties)
            {
                Console.WriteLine("- Property: "+prop.type+" | Value: "+prop.value);
            }
        }
    }

    public enum ModObjectType { NONE, ITEM, RECIPE, TECHNOLOGY, TILE, ENTITY }

    public class PropertyInfo
    {
        public required string type;
        public required bool isNeeded;
        public List<ModObjectType> allowedModObjects = new();
        public bool editable = true;
        public DataGridInfo gridInfo = new();

        public object value = string.Empty;

        public void SetValue(string value)
        {
            this.value = value;
        } 
    }

    public class DataGridInfo
    {
        public string valueBoxType = "text";
    }

    internal class ModObjectConstructor
    {
        // Hier sind alle möglichen Properties vordefiniert
        PropertyInfo[] propertyInfos =
        {
            new()
            {   
                type = "name",
                isNeeded = true,
                allowedModObjects = {ModObjectType.ITEM, ModObjectType.RECIPE, ModObjectType.TECHNOLOGY, ModObjectType.TILE, ModObjectType.ENTITY},
                gridInfo = new()
                {
                    valueBoxType = "text",
                }
            },
            new()
            {
                type = "icon",
                isNeeded = true,
                allowedModObjects = {ModObjectType.ITEM, ModObjectType.RECIPE, ModObjectType.TECHNOLOGY, ModObjectType.TILE, ModObjectType.ENTITY},
                gridInfo = new()
                {
                    valueBoxType = "image",
                }
            },
            new()
            {
                type = "icon_size",
                isNeeded = true,
                allowedModObjects = {ModObjectType.ITEM, ModObjectType.RECIPE, ModObjectType.TECHNOLOGY, ModObjectType.TILE, ModObjectType.ENTITY}
            },
            new()
            {
                type = "description",
                isNeeded = true,
                allowedModObjects = {ModObjectType.ITEM, ModObjectType.RECIPE, ModObjectType.TECHNOLOGY, ModObjectType.TILE, ModObjectType.ENTITY}
            },
            new()
            {
                type = "stack_size",
                isNeeded = true,
                allowedModObjects = {ModObjectType.ITEM}
            },
            new()
            {
                type = "type",
                isNeeded = true,
                allowedModObjects = {ModObjectType.ITEM},
                editable = false
            },
            new()
            {
                type = "durability",
                isNeeded = false,
                allowedModObjects = {ModObjectType.ITEM}
            },
            new()
            {
                type = "value",
                isNeeded = false,
                allowedModObjects = {ModObjectType.ITEM}
            },
            new()
            {
                type = "effects",
                isNeeded = false,
            },
            new()
            {
                type = "recipe",
                isNeeded = false,
                allowedModObjects = {ModObjectType.ITEM}
            },
            new()
            {
                type = "equipable",
                isNeeded = false,
                allowedModObjects = {ModObjectType.ITEM}
            },
            new()
            {
                type = "use_effects",
                isNeeded = false,
            },
            new()
            {
                type = "unlocks",
                isNeeded = false,
                allowedModObjects = {ModObjectType.TECHNOLOGY}
            },
            new()
            {
                type = "category",
                isNeeded = false,
                allowedModObjects = {ModObjectType.ITEM}
            },
            new()
            {
                type = "price",
                isNeeded = false,
                allowedModObjects = {ModObjectType.ITEM}
            },
            new()
            {
                type = "rarity",
                isNeeded = false,
                allowedModObjects = {ModObjectType.ITEM}
            },
        };

        public List<ModObject> modObjects = new();
        public ModObject CreateModObject(ModObjectType modObjectType)
        {
            ModObject modObject = new()
            {
                type = modObjectType,
                id = MainWindow.modObjectsCount
            };
            
            modObjects.Add(modObject);
            MainWindow.modObjectsCount++;
            return modObject;
        }

        public ModObject SetNeededProperties(ModObject modObject)
        {
            foreach(var prop in propertyInfos)
            {
                if (prop.isNeeded && prop.allowedModObjects.Contains(modObject.type))
                {
                    modObject.properties.Add(prop);
                }
            }
            return modObject; 
        }
    }
}

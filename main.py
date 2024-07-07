import pygame
import sys
from constants import *
import scene
from rect import Rect
from rect import TextInputField
import eventmanager
from mod import *


def handle_events(scene):
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            return False
        if event.type == pygame.MOUSEBUTTONDOWN:
            for obj in scene.current_scene.active_objects:
                obj.handle_click()
    return True


def main():
    pygame.init()
    screen = pygame.display.set_mode(WINDOW_SIZE)
    pygame.display.set_caption("Factorio Mod Manager")

    scene.current_scene = scene.Scene("main_menu")

    # Left side bar
    sidebar = Rect(scene.current_scene, screen)
    sidebar.transform.position = (0, 0)
    sidebar.transform.size = (45, 600)
    sidebar.color = (50, 50, 50)

    # Add object button
    button_1 = Rect(scene.current_scene, screen)
    button_1.transform.position = (0, 0)
    button_1.transform.size = (45, 45)
    button_1.texture = pygame.transform.scale(pygame.image.load('textures/plus.png'), (button_1.transform.size[0], button_1.transform.size[1]))
    button_1.add_click_callback(create_project)
    button_1.hover_function = True

    # Project name input
    project_name_input_field = TextInputField(scene.current_scene, screen, "Test")

    running = True
    while running:
        running = handle_events(scene)

        eventmanager.update(scene.current_scene)  # Pass scene as parameter

        screen.fill(COLOR_BACKROUND)

        for obj in scene.current_scene.active_objects:
            obj.draw()

        pygame.display.flip()

    pygame.quit()
    sys.exit()

if __name__ == "__main__":
    main()

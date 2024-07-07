import pygame
from constants import WINDOW_SIZE
from constants import FONT_SIZE
class Transform:
    def __init__(self):
        self.position = (WINDOW_SIZE[0]/2, WINDOW_SIZE[1]/2)
        self.size = (100, 100)

class Rect:
    def __init__(self, scene, screen):
        self.transform = Transform()
        self.color = (255, 255, 255)
        scene.active_objects.append(self)
        self.texture = None
        self.hover_function = False
        self.mouse_hover = False
        self.click_callbacks = []
        self.rect = pygame.Rect(self.transform.position[0], self.transform.position[1], self.transform.size[0], self.transform.size[1])
        self.screen = screen

    def draw(self):
        self.rect = pygame.Rect(self.transform.position[0], self.transform.position[1], self.transform.size[0], self.transform.size[1])
        pygame.draw.rect(self.screen, self.color, self.rect)
        if self.texture is not None:
            self.screen.blit(self.texture, self.rect.topleft)
        self.update_hover()

    def update_hover(self):
        if not self.hover_function:
            return
        mouse_pos = pygame.mouse.get_pos()
        self.mouse_hover = self.rect.collidepoint(mouse_pos)
        if self.mouse_hover:
            self.color = (150, 150, 150)  # Change color when hovered
        else:
            self.color = (100, 100, 100)  # Default color

    def add_click_callback(self, callback):
        self.click_callbacks.append(callback)

    def handle_click(self):
        if self.mouse_hover:
            for callback in self.click_callbacks:
                callback()

class TextInputField:
    def __init__(self, scene, screen, initial_text='', color_active=(0, 0, 0), color_inactive=(150, 150, 150)):
        self.transform = Transform()
        self.color = color_inactive
        scene.active_objects.append(self)

        self.rect = pygame.Rect(self.transform.position[0], self.transform.position[1], self.transform.size[0], self.transform.size[0])
        self.color_active = color_active
        self.color_inactive = color_inactive
        self.text = initial_text
        self.font = pygame.font.SysFont('Arial', FONT_SIZE)
        self.txt_surface = self.font.render(initial_text, True, self.color)
        self.active = False
        self.screen = screen

    def handle_event(self, event):
        if event.type == pygame.MOUSEBUTTONDOWN:
            # Toggle the active variable.
            if self.rect.collidepoint(event.pos):
                self.active = not self.active
            else:
                self.active = False
            # Change the color of the input box.
            self.color = self.color_active if self.active else self.color_inactive

        if event.type == pygame.KEYDOWN:
            if self.active:
                if event.key == pygame.K_RETURN:
                    print(self.text)
                    self.text = ''
                elif event.key == pygame.K_BACKSPACE:
                    self.text = self.text[:-1]
                else:
                    self.text += event.unicode
                # Re-render the text.
                self.txt_surface = self.font.render(self.text, True, self.color)

    def draw(self):
        # Blit the text.
        self.screen.blit(self.txt_surface, (self.rect.x + 5, self.rect.y + 5))
        # Blit the rect.
        pygame.draw.rect(self.screen, self.color, self.rect, 2)

    def update(self):
        # Resize the box if the text is too long.
        width = max(200, self.txt_surface.get_width() + 10)
        self.rect.w = width

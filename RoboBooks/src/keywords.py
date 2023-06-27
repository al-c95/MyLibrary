from automation import MyLibraryAutomation as auto
from typing import List

from robot.api.deco import keyword, library

ROBOT_AUTO_KEYWORDS = False

@library
class RoboBooks:

    @keyword
    def init_main_window(app_version : str):
        auto.init_main_window(app_version);

    @keyword
    def click_add_button():
        auto.click_toolbar_button('toolStripButton1');

    @keyword
    def click_find_book_via_api_button():
        auto.click_toolbar_button('toolStripButton2');

    @keyword
    def click_delete_button():
        auto.click_toolbar_button('toolStripButton3');

    @keyword
    def click_manage_tags_button():
        auto.click_toolbar_button('toolStripButton4');

    @keyword
    def click_wishlist_button():
        auto.click_toolbar_button('toolStripButton5');

    @keyword
    def click_manage_item_tags_button():
        auto.click_item_details_button('Manage Tags');

    @keyword
    def click_manage_item_copies_button():
        auto.click_item_details_button('Manage Copies');

    @keyword
    def click_select_image_button():
        auto.click_item_details_button('Select Image');

    @keyword
    def click_remove_image_button():
        auto.click_item_details_button('Remove Image');

    @keyword
    def click_save_changes_button():
        auto.click_item_details_button('Save Changes');

    @keyword
    def click_discard_changes_button():
        auto.click_item_details_button('Discard Changes');

    @keyword
    def get_notes_field_text():
        return auto.get_notes_field_text();

    @keyword
    def set_notes_field_text(text : str):
        auto.set_notes_field_text(text);

    @keyword
    def select_category(category : auto.Categories):
        auto.select_category(category);

    @keyword
    def select_category_books():
        auto.select_category(auto.Categories.Book);

    @keyword
    def select_category_media():
        auto.select_category(auto.Categories.Media);

    @keyword
    def select_category_cd():
        auto.select_category(auto.Categories.Cd);

    @keyword
    def select_category_dvd():
        auto.select_category(auto.Categories.Dvd);

    @keyword
    def select_category_bluray():
        auto.select_category(auto.Categories.Bluray);

    @keyword
    def select_category_vhs():
        auto.select_category(auto.Categories.Vhs);

    @keyword
    def select_category_vinyl():
        auto.select_category(auto.Categories.Vinyl);

    @keyword
    def select_category_flash_drive():
        auto.select_category(auto.Categories.FlashDrive);

    @keyword
    def select_category_floppy_disk():
        auto.select_category(auto.Categories.FloppyDisk);

    @keyword
    def select_category_others():
        auto.select_category(auto.Categories.Other);

    @keyword
    def select_item_in_list_by_title(title : str):
        auto.select_item_in_list_by_title(title);
    
    @keyword
    def apply_filters():
        auto.apply_filters();

    @keyword
    def clear_filters():
        auto.clear_filters();

    @keyword
    def add_filter_tag(tag : str):
        auto.add_filter_tag(tag);

    @keyword
    def remove_filter_tag(tag : str):
        return auto.remove_filter_tag(tag);

    @keyword
    def filter_by_title(title : str):
        auto.filter_by_title(title);

    @keyword
    def clear_title_filter():
        auto.filter_by_title('');

    @keyword
    def get_status_bar_text():
        return auto.get_status_bar_text();

    @keyword
    def click_menu_item(menu_items : List[str]):
        return auto.click_menu_item(menu_items);

    @keyword
    def take_image_screenshot(file_path : str):
        auto.take_image_screenshot(file_path);

    @keyword
    def take_data_grid_view_screenshot(file_path : str):
        auto.take_image_screenshot(file_path);

# testing
if __name__ == '__main__':
    RoboBooks.init_main_window('1.4.0');
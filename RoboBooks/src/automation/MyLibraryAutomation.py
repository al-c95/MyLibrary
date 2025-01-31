from unicodedata import name
import uiautomation as ui
import enum
import time
from typing import List
import re
import pyautogui

SHOW_WINDOW_TIMEOUT = 3; # sec

windows = {
    'main' : None
}


class Categories(enum.Enum):
    Book = 1
    Media = 2
    Cd = 3
    Dvd = 4
    Bluray = 5
    Vhs = 6
    Vinyl = 7
    FlashDrive = 8
    FloppyDisk = 9
    Other = 10


def init_main_window(app_version : str):

    found_window = False;
    window_name = 'MyLibrary ' + app_version;

    for control, depth in ui.WalkTree(ui.GetRootControl(), getFirstChild=_get_first_child, getNextSibling=_get_next_sibling, includeTop=True, maxDepth=1):
        if control.ControlType == ui.ControlType.WindowControl:
            if control.GetPropertyValue(ui.PropertyId.LegacyIAccessibleNameProperty) == window_name:
                found_window = True;
                windows['main'] = control;
                
                title_bar = control.TitleBarControl();
                print(title_bar.GetPropertyValue(ui.PropertyId.LegacyIAccessibleValueProperty));

    if not found_window:
        raise Exception('Could not find application window');
    else:
        print('Found application window...');


def _get_first_child(control):
    return control.GetFirstChildControl();


def _get_next_sibling(control):
    return control.GetNextSiblingControl();


def click_toolbar_button(btn_name : str):
    app_window = windows['main'];
    app_window.SetActive();

    toolbar = app_window.ToolBarControl(searchDepth=3, name='toolStrip1');

    for control, depth in ui.WalkControl(toolbar):
        if control.ControlType == ui.ControlType.ButtonControl:
            if control.Name == btn_name:
                control.Click();


def _get_item_details_group():
    app_window = windows['main'];
    app_window.SetActive();

    return app_window.GroupControl(searchDepth=3, name='Item Details');


def click_item_details_button(btn_name : str):
    item_details_group = _get_item_details_group();

    for control, depth in ui.WalkControl(item_details_group):
        if control.ControlType == ui.ControlType.ButtonControl:
            if control.Name == btn_name:
                control.Click();
                return;

    raise Exception('Invalid item details group button: ' + btn_name);


def get_notes_field_text() -> str:
    item_details_group = _get_item_details_group();

    notes_field = item_details_group.EditControl(searchDepth=1, automationId='textBoxNotes');

    return notes_field.GetPropertyValue(ui.PropertyId.LegacyIAccessibleValueProperty);


def set_notes_field_text(text : str):
    item_details_group = _get_item_details_group();

    notes_field = item_details_group.EditControl(searchDepth=1, automationId='textBoxNotes');
    notes_field.SendKeys(text);


def select_category(category : Categories):
    app_window = windows['main'];
    app_window.SetActive();

    categories_combo_box = app_window.ComboBoxControl(searchDepth=3, automationId='categoryDropDown');
    if category == Categories.Book:
        categories_combo_box.Select('Book');
    elif category == Categories.Media:
        categories_combo_box.Select('Media Items (all types)');
    elif category == Categories.Cd:
        categories_combo_box.Select('Cd');
    elif category == Categories.Dvd:
        categories_combo_box.Select('Dvd');
    elif category == Categories.Bluray:
        categories_combo_box.Select('BluRay');
    elif category == Categories.Vhs:
        categories_combo_box.Select('Vhs');
    elif category == Categories.Vinyl:
        categories_combo_box.Select('Vinyl');
    elif category == Categories.FlashDrive:
        categories_combo_box.Select('Flash Drive');
    elif category == Categories.FloppyDisk:
        categories_combo_box.Select('Floppy Disk');
    elif category == Categories.Other:
        categories_combo_box.Select('Other');


def _find_item_in_list_by_title(title : str, select_if_found : bool):
    app_window = windows['main'];
    app_window.SetActive();

    found = False;

    items_table = app_window.TableControl(searchDepth=3, automationId='dataGrid');
    rows = items_table.GetChildren();

    if not len(rows) == 0:
        for row in rows[1:]:
            title_field = row.GetChildren()[2];
            if title_field.GetPropertyValue(ui.PropertyId.LegacyIAccessibleValueProperty) == title:
                if select_if_found:
                    row.Click();
                found = True;

    return found;

# TODO: return selected row information
def select_item_in_list_by_title(title : str) -> bool:
    return _find_item_in_list_by_title(title, True);


def item_exists_in_list_with_title(title : str) -> bool:
    return _find_item_in_list_by_title(title, False);


def _get_filter_group():
    app_window = windows['main'];
    app_window.SetActive();

    for control, depth in ui.WalkTree(app_window, getFirstChild=_get_first_child, getNextSibling=_get_next_sibling, includeTop=True, maxDepth=4):
        if control.ControlType == ui.ControlType.GroupControl:
            if control.GetPropertyValue(ui.PropertyId.LegacyIAccessibleNameProperty) == 'Filter':
                filter_group = control;

                return filter_group;


def _click_filter_group_button(button_index : int):
    filter_group = _get_filter_group();
    apply_filter_button = filter_group.GetChildren()[button_index];
    apply_filter_button.Click();


def apply_filters():
    _click_filter_group_button(6);


def clear_filters():
    _click_filter_group_button(5);


def add_filter_tag(tag : str):
    filter_group = _get_filter_group();
    tags_combo_box = filter_group.ComboBoxControl(searchDepth=1, automationId='addFilterTagField');
    tags_combo_box.SendKeys(tag);

    _click_filter_group_button(3);


def remove_filter_tag(tag_name : str) -> bool:
    filter_group = _get_filter_group();
    tags_list = filter_group.ListControl(searchDepth=1, automationId='filterTagsList');

    tags = tags_list.GetChildren();
    for tag in tags[1:]:
        if tag.GetChildren()[0].GetPropertyValue(ui.PropertyId.LegacyIAccessibleNameProperty) == tag_name:
            print('found');
            tag.Click();
            _click_filter_group_button(2);

            return True;

    return False;


def filter_by_title(title : str):
    filter_group = _get_filter_group();
    title_filter_field = filter_group.EditControl(searchDepth=1, automationId='titleFilterField');
    title_filter_field.SendKeys(title);


def get_status_bar_text() -> str:
    app_window = windows['main'];
    app_window.SetActive();

    status_bar = app_window.StatusBarControl(searchDepth=3, automationId='statusStrip');
    label1 = status_bar.GetChildren()[0];
    label2 = status_bar.GetChildren()[1];

    text = label1.GetPropertyValue(ui.PropertyId.LegacyIAccessibleNameProperty) + ' ' + label2.GetPropertyValue(ui.PropertyId.LegacyIAccessibleNameProperty);

    return text;


def click_menu_item(menu_items : List[str]):
    app_window = windows['main'];
    app_window.SetActive();

    if len(menu_items)==0:
        return;
    else:
        menu_strip = app_window.MenuBarControl(searchDepth=3, name='menuStrip1');
        for menu in menu_strip.GetChildren():
            if menu.GetPropertyValue(ui.PropertyId.LegacyIAccessibleNameProperty) == menu_items[0]:
                menu.GetInvokePattern().Invoke();
                if len(menu_items) > 1:
                    menu_items.pop(0);
                    _click_menu_item(menu,menu_items);


def _click_menu_item(menu_item : ui.MenuItemControl, menu_items : List[str]):
    if len(menu_items)==0:
        return;
    else:
        for sub_menu_item in menu_item.GetChildren():
            if sub_menu_item.GetPropertyValue(ui.PropertyId.LegacyIAccessibleNameProperty) == menu_items[0]:
                sub_menu_item.GetInvokePattern().Invoke();
                if len(menu_items) > 1:
                    menu_items.pop(0);
                    _click_menu_item(sub_menu_item,menu_items);


def _read_database_statistics():
    app_window = windows['main'];
    app_window.SetActive();

    stats_dialog = app_window.WindowControl(searchDepth=1, automationId='statsDialog');
    if not stats_dialog.Exists(1,1):
        click_menu_item(['View', 'Database statistics']);

    if ui.WaitForExist(stats_dialog, SHOW_WINDOW_TIMEOUT):
        stats_info_box = stats_dialog.DocumentControl(searchDepth=1, automationId='statsBox');
        stats_info_text = stats_info_box.GetPropertyValue(ui.PropertyId.LegacyIAccessibleValueProperty);
        # BUG: text is incorrect
        # TEST
        print(stats_info_text);
        
        regex = re.compile('^Books: \d+\nPublishers: \d+\nAuthors: \d+\n\nMedia Items: \d+\n\nTags: \d+\n$');
        match = regex.match(str(stats_info_text));
        if not bool(match):
            raise Exception('Database statistics dialog string has incorrect format.');
        else:
            pass;
            # TODO: return a dictionary of number of items per type       
    else:
        raise Exception('Database statistics dialog failed to appear after ' + SHOW_WINDOW_TIMEOUT + ' seconds.');


def _get_image_bounding_rectangle():
    app_window = windows['main'];
    app_window.SetActive();

    item_details_group = _get_item_details_group();
    image = item_details_group.GetChildren()[9];
    
    return image.BoundingRectangle;


def _get_data_grid_view_bounding_rectangle():
    app_window = windows['main'];
    app_window.SetActive();

    items_table = app_window.TableControl(searchDepth=3, automationId='dataGrid');

    return items_table.BoundingRectangle;


def take_image_screenshot(file_path : str):
    app_window = windows['main'];
    app_window.SetActive();

    item_details_group = _get_item_details_group();
    image = item_details_group.GetChildren()[9];

    image.CaptureToImage(file_path);


def take_data_grid_view_screenshot(file_path : str):
    app_window = windows['main'];
    app_window.SetActive();

    items_table = app_window.TableControl(searchDepth=3, automationId='dataGrid');

    items_table.CaptureToImage(file_path);

# testing
if __name__ == '__main__': 
    init_main_window('1.4.0');
    
    filter_by_title('UBD');
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TranslateUI : MonoBehaviour
{
    [Header("바꾸어줄 Text")]
    [SerializeField] Text text_setting;
    [SerializeField] Text text_menu;
    [SerializeField] Text text_Language;
    [SerializeField] Text text_resolution;
    [SerializeField] Text text_sound;
    [SerializeField] Text text_credits;
    [SerializeField] Text text_Language2;
    [SerializeField] Text text_Back;
    [SerializeField] Text text_Select;

    public void Translate_Korean()
    {
        text_setting.text = "설정";
        text_menu.text = "메뉴";
        text_Language.text = "언어";
        text_resolution.text = "해상도";
        text_sound.text = "소리";
        text_credits.text = "크레딧";
        text_Language2.text = "언어";
        text_Back.text = "뒤로";
        text_Select.text = "선택";
    }

    public void Translate_English()
    {
        text_setting.text = "Settings";
        text_menu.text = "Menu";
        text_Language.text = "Language";
        text_resolution.text = "Resolution";
        text_sound.text = "Sound";
        text_credits.text = "Credits";
        text_Language2.text = "Language";
        text_Back.text = "Back";
        text_Select.text = "Select";
    }

    public void Translate_Chinese()
    {
        text_setting.text = "设置";
        text_menu.text = "菜单";
        text_Language.text = "语言";
        text_resolution.text = "分辨率";
        text_sound.text = "声音";
        text_credits.text = "制作人员";
        text_Language2.text = "语言";
        text_Back.text = "返回";
        text_Select.text = "选择";
    }

    public void Translate_Spanish()
    {
        text_setting.text = "Configuración";
        text_menu.text = "Menú";
        text_Language.text = "Idioma";
        text_resolution.text = "Resolución";
        text_sound.text = "Sonido";
        text_credits.text = "Créditos";
        text_Language2.text = "Idioma";
        text_Back.text = "Atrás";
        text_Select.text = "Seleccionar";
    }

}

/// <reference path="jquery-3.2.1.js" />
/// <reference path="jquery.validate.js" />
/// <reference path="jquery.validate.unobtrusive.js" />
//Cała walidacja opiera się na atrybutach data- z html5.
//Stworzenie metody walidacyjnej ("nazwa metody", "(wartośćElementu, element, dodatkoweArgumenty) {ciało metody validacyjnej}")
jQuery.validator.addMethod('requirediftrue', function (value, element, params) {
    var checkbox = $('#' + params).first(); //Pobieranie elementu checkboxa (id z params)
    var checkboxValue = checkbox.is(":checked"); //Sprawdzenie czy jest aznaczony
    return !checkboxValue || value; //Jeśli nie jest zaznaczony zawsze true, jeśli zaznaczony - value musi być true żeby validacja przeszła
}, '');

//Dodanie adaptera do walidatora ("jaka metoda/nazwa atrybutu", "dodatkowe parametry")
jQuery.validator.unobtrusive.adapters.addSingleVal("requirediftrue", "boolprop");
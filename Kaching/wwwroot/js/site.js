﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#monthdropdown')
        .dropdown()
    ;

$('.togglecomment')
        .popup()
    ;

// init modal
$('#settlementmodal').click(function () {
    $('.ui.modal').modal('show');
});



$('.paymentremindertooltip')
        .popup()
    ;

$('#categorydropdown')
        .dropdown()
    ;


$('#paymentstatusdropdown')
        .dropdown()
    ;

$('#buyerdropdown')
        .dropdown()
    ;

$('#date_calendar')
        .calendar({
            type: 'date'
        })
    ;
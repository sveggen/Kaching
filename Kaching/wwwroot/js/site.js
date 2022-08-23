// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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

$('#frequencydropdown')
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

$('#receiverdropdown')
        .dropdown()
    ;

$('#month_year_calendar')
        .calendar({
            type: 'month'
        })
    ;

$('#rangestart').calendar({
    type: 'date',
    endCalendar: $('#rangeend')
});

$('#add_group_members')
    .dropdown({
        minSelections: 1,
        maxSelections: 10
    })
;
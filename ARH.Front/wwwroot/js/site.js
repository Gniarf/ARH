// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})
function goToNextCombo(select) {
    var nextSelect;
    var currentCell = $(select).closest('td');
    var currentRow = $(select).closest('tr');
    var firstday = currentRow.find('td:first');
    var lastCell = currentRow.find('td:last');
    var isLastFriday = currentCell.hasClass('vendredi') && currentCell.is(lastCell) && firstday.hasClass('table-success');
    var isLastDayOfWeek = currentCell.hasClass('vendredi') && currentCell.index() === (lastCell.index() - 3) && firstday.hasClass('table-success');
    var islast = currentCell.index() === (lastCell.index() - 1);
    if (isLastFriday) {
        var nextRow = currentRow.next();
        nextSelect = nextRow.find('td:eq(2) select');
    } else if (isLastDayOfWeek) {
        var nextRow = currentRow.next();
        nextSelect = nextRow.find('td:eq(2) select');
    } else if (islast) {
        var nextRow = currentRow.next();
        nextSelect = nextRow.find('td:eq(0) select');
    } else if (currentCell.hasClass('vendredi')) {
        nextSelect = $(select).closest('td').nextAll('td').eq(2).find('select');
    } else if (currentCell.index() === 2) {
        nextSelect = currentCell.next('td').find('select');
    }
    if (nextSelect && nextSelect.length) {
        nextSelect.focus();
    }
}
function setupTable() {
    const cells = document.querySelectorAll('table td select');
    cells.forEach((cell) => {
        cell.addEventListener('change', (event) => {
            sum(event.target);
        });
    });
}

function sum(select) {
    var row = select.closest('tr');
    var totalDaysElement = document.getElementById('total-days-' + row.dataset.row);
    var cellstrav = row.getElementsByClassName('joursur');
    var nombrejour = 0;
    for (var i = 0; i < cellstrav.length; i++) {
        nombrejour++;
    }
    var vencell = row.getElementsByClassName('vendredi');
    for (var i = 0; i < vencell.length; i++) {
        nombrejour++;
    }
    console.log(nombrejour);
    var totalDays = 0;
    var cells = row.querySelectorAll('select');
    cells.forEach((cell) => {
        totalDays = totalDays + parseInt(cell.value);
    });
    totalDaysElement.innerText = (totalDays / 100.0).toFixed(2);
  
    if (nombrejour < (totalDays/100.0)) {
        totalDaysElement = totalDaysElement.style.background = 'red';

    }
    if (nombrejour == (totalDays/100.0)) {
        totalDaysElement = totalDaysElement.style.background = '#00FF00';

    }
}
function updatesomme() {
    var totalDaysElements = document.querySelectorAll('[id^=total-days]');
    var sum = 0;
    for (var i = 0; i < totalDaysElements.length; i++) {
        sum += parseFloat(totalDaysElements[i].innerText);
    }
    return sum;
}


var calendar = document.getElementById('calendar');
function sumcol(select) {
    var columnNumber = select.getAttribute("ColumnNumber");
    var concernedSelects = $('*[columnnumber="' + columnNumber + '"]');
    var totalDays = 0;
    if (concernedSelects != null) {
        concernedSelects.each(x => {
            var val = concernedSelects.get(x).value;
            var intval = parseInt(val);
            if (intval !== undefined && !isNaN(intval)) {
                totalDays += parseInt(intval);
            }
        });
    }
    var columnTotals = calendar.getElementsByClassName("columnTotal");
    for (var i = 0; i < columnTotals.length; i++) {
        var item = columnTotals[i];
        if (item.getAttribute('columnnumber') === columnNumber) {
            if (totalDays > 100) {
                item.innerHTML = (totalDays / 100.0).toFixed(2);
                item = item.style.background = "red";
            } else if (totalDays < 100) {
                item.innerHTML = (totalDays / 100.0).toFixed(2);
                item = item.style.background = "#FA8072";

            } else if (totalDays == 100) {
                item.innerHTML = (totalDays / 100.0).toFixed(2);
                item = item.style.background = "#00FF00";

            }
            return;
        }
    }
}


function nextSelect(select) {
    $(select).closest('td').next('td').find('select').focus();
    goToNextCombo(select);
    setupTable();
    sum(select);
    sumcol(select);
    var totalDays = updatesomme();
    var totalDaysElements = document.getElementById('table-days');
    totalDaysElements.innerText = totalDays.toFixed(2);
    var row = select.closest('tr');
    var cells = row.getElementsByClassName('joursur');
    var nombrejour = 0;
    for (var i = 0; i < cells.length; i++) {
        nombrejour++;
    }
    var vencell = row.getElementsByClassName('vendredi');
    for (var i = 0; i < vencell.length; i++) {
        nombrejour++;
    }
    if (nombrejour > totalDays) {
        totalDaysElements = totalDaysElements.style.background = '#FA8072';

    }
    if (nombrejour < totalDays) {
        totalDaysElements = totalDaysElements.style.background = 'red';

    }
    if (nombrejour == totalDays) {
        totalDaysElements = totalDaysElements.style.background = '#00FF00';

    }

}
document.addEventListener('DOMContentLoaded', function () {
    var submitBtn = document.getElementById('submit-btn');
    submitBtn.addEventListener('click', function (event) {
        var tableDays = document.getElementById('table-days');
        var columnTotalElements = document.getElementsByClassName('columnTotal');
        var erreurMessage = document.getElementById('erreur-superieur');
        var commentField = document.getElementById('commentaire');
        var erreurcomment = document.getElementById('erreur-commentaire');
        var allowsubmit = true;
        for (var i = 0; i < columnTotalElements.length; i++) {
            var columnTotal = columnTotalElements[i];
            if ((tableDays.style.backgroundColor == 'red' || columnTotal.style.backgroundColor == 'red') && commentField.value.trim() === '') {
                erreurMessage.style.display = 'block';
                erreurcomment.style.display = 'block';
                allowsubmit = false;
                break; 
            }
            }
            if (!allowsubmit) {
                event.preventDefault();
            }

        });
});


document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("selectedMonth").addEventListener("change", function () {
        this.form.submit();
    })
});

window.addEventListener('load', function() {
    var selects = document.getElementsByClassName('dropdown');
    for (var i = 0; i < selects.length; i++) {
        nextSelect(selects[i]);
    }
});


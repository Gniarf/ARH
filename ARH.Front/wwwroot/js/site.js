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
    var nextCell = currentCell.next();
    var isLastFriday = currentCell.hasClass('vendredi') && currentCell.is(lastCell) && firstday.hasClass('table-success');
    var isLastDayOfWeek = currentCell.hasClass('vendredi') && currentCell.index() === (lastCell.index() - 3) && firstday.hasClass('table-success');
    var islast = currentCell.index() === (lastCell.index() - 1);
    if (nextCell.hasClass('table-warning')) {
        nextCell = nextCell.next();
        nextSelect = nextCell.find('select');
    }
    else if (isLastFriday) {
        var nextRow = currentRow.next();
        nextSelect = nextRow.find('td:eq(2) select');
    } else if (isLastDayOfWeek) {
        var nextRow = currentRow.next();
        nextSelect = nextRow.find('td:eq(2) select');
    } else if (islast) {
        var nextRow = currentRow.next();
        nextSelect = nextRow.find('td:eq(0) select');
    } else if (currentCell.hasClass('vendredi')) {
        var nextCell = currentCell.nextAll('td').eq(2);
        var nextCellSelect = nextCell.find('select');

         if (nextCellSelect.length) {
            if (nextCell.hasClass('table-warning')) {
                nextSelect = currentCell.nextAll('td').eq(3).find('select');
            } else {
                nextSelect = currentCell.nextAll('td').eq(2).find('select');
            }
        } else {
            nextSelect = null; // Ne pas sauter la colonne
        }

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
    

    
  if (nombrejour < (totalDays / 100.0)) {
        totalDaysElement = totalDaysElement.style.background = 'red';

    }
    if (nombrejour == (totalDays / 100.0)) {
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
    var columnElement = document.querySelector('.columnTotal[columnnumber="' + columnNumber + '"]');
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

            } else if (totalDays == 100  ) {
                item.innerHTML = (totalDays / 100.0).toFixed(2);
                if( $(select).closest('td').hasClass("table-warning") || $(select).closest('td').hasClass("table-success") ){
                   item.style.background = "red";
                }else{
                 item.style.background = "#00FF00";
                }
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
        var commentField = document.getElementById('Comment');
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

window.addEventListener('load', function () {
    var selects = document.getElementsByClassName('dropdown');
    for (var i = 0; i < selects.length; i++) {
        nextSelect(selects[i]);
    }
});




document.addEventListener("DOMContentLoaded", function () {
    var totalDaysSum = 0;
    var totalDaysSum1 = 0;
    var totalDaysSum2 = 0;
    var totalDaysSum3 = 0;
    var totalDaysSum4 = 0;
    var totalDaysSum5 = 0;
    var totalDayColumn = 0;
    var columnTotalCells = document.getElementsByClassName("columnTotal");
    var tableDaysCell = document.getElementById("table-days");
    for (var i = 1; i <= 5; i++) {
        for (var j = 0; j < columnTotalCells.length; j++) {
            if (i == 1) {
                var totalCells = document.getElementById('total-days-' + i);
                var row = document.getElementById('row' + i + '-' + j);
                totalDaysSum1 += parseFloat(row.innerText.replace(',', '.'));
                totalCells.innerText = totalDaysSum1;
            } else if (i == 2) {
                var totalCells = document.getElementById('total-days-' + i);
                var row = document.getElementById('row' + i + '-' + j);
                totalDaysSum2 += parseFloat(row.innerText.replace(',', '.'));
                totalCells.innerText = totalDaysSum2;
            } else if (i == 3) {
                var totalCells = document.getElementById('total-days-' + i);
                var row = document.getElementById('row' + i + '-' + j);
                totalDaysSum3 += parseFloat(row.innerText.replace(',', '.'));
                totalCells.innerText = totalDaysSum3;
            } else if (i == 4) {
                var totalCells = document.getElementById('total-days-' + i);
                var row = document.getElementById('row' + i + '-' + j);
                totalDaysSum4 += parseFloat(row.innerText.replace(',', '.'));
                totalCells.innerText = totalDaysSum4;
            } else if (i == 5) {
                var totalCells = document.getElementById('total-days-' + i);
                var row = document.getElementById('row' + i + '-' + j);
                totalDaysSum5 += parseFloat(row.innerText.replace(',', '.'));
                totalCells.innerText = totalDaysSum5;
            }

        }
    }
    totalDaysSum = totalDaysSum1 + totalDaysSum2 + totalDaysSum3 + totalDaysSum4 + totalDaysSum5;

    tableDaysCell.innerText = totalDaysSum;
    var calendar = document.getElementById('calendar');
    var columnTotalCells = document.getElementsByClassName("columnTotal");


    for (var i = 0; i < columnTotalCells.length; i++) {
        var columnNumber = columnTotalCells[i].getAttribute("columnnumber");
        var cellsInColumn = calendar.querySelectorAll('[columnnumber="' + columnNumber + '"]');
        var total = 0;

        for (var j = 0; j < cellsInColumn.length; j++) {
            var val = cellsInColumn[j].innerText;
            var intval = parseFloat(val.replace(',', '.'));
            if (!isNaN(intval)) {
                total += parseFloat(intval);

            }

        }

        for (var k = 0; k < columnTotalCells.length; k++) {
            if (columnTotalCells[k].getAttribute("ColumnNumber") === columnNumber & columnTotalCells[k].innerHTML == 0) {
                columnTotalCells[k].innerHTML = total;
                var item = columnTotalCells[k];
                if (total > 1) {
                    item.innerHTML = total;
                    item = item.style.background = "red";
                } else if (total < 1) {
                    item.innerHTML = total;
                    item = item.style.background = "#FA8072";

                } else if (total == 1) {
                    item.innerHTML = total;
                    item = item.style.background = "#00FF00";

                }
                break;
            }
        }
    }
    var nombrejour = 0
    for (var j = 0; j < columnTotalCells.length; j++) {
        var rowz = document.getElementById('row1-' + j);
        if (rowz.classList.contains('joursur')) {
            nombrejour++;
        }
        if (rowz.classList.contains('vendredi')) {
            nombrejour++;
        }
    }
    if (nombrejour > totalDaysSum) {
        tableDaysCell = tableDaysCell.style.background = '#FA8072';

    }
    if (nombrejour < totalDaysSum) {
        tableDaysCell = tableDaysCell.style.background = 'red';

    }
    if (nombrejour == totalDaysSum) {
        tableDaysCell = tableDaysCell.style.background = '#00FF00';

    }





});



document.addEventListener("DOMContentLoaded", ready);

function ready() {

    function init() {
        document.querySelector("#car-type").addEventListener("change", showCarTypeList);
        document.querySelector("#book-car").addEventListener("click", bookCar);
    }

    // car type selection han changed
    function showCarTypeList(e) {

        var carType = e.target.value;

        if (carType === 0)
            return;

        // disable booking button since we have not yet selected a car
        //$('#book-selection').prop('disabled', true);

        var url = "/Home/GetCarList";
        var data = {
            carType: carType
        }

        $.ajax(
            {
                type: 'GET',
                url: url,
                dataType: 'json',
                cache: false,
                data: data,
                contentType: "application/json; charset=utf-8"
            }).fail(function (jqXHR, textStatus, errorThrown) {
                alert(errorThrown + this.url);
            }).done(function (carList, textStatus, jqXHR) {

                // build and display html table with carList
                buildCarsTable(carList);
            });
    };


    function buildCarsTable(carList) {

        let th = `<th style="padding:5px 20px">`;
        var table = `<table style="border:1px solid #00000e20; padding:0 2px;font-size:14px"><thead><tr>${th}Internal Id</th>${th}Registration</th>${th}Description</th></tr></thead><tbody>`;

        // add a row at the time as we iterate through list
        let car;
        for (car in carList) {
            let id = carList[car].id;
            let description = carList[car].description;
            let registration = carList[car].registration;
            let td = `<td style="padding: 5px 20px; color:#000">`;
            let td_id = `<td style="padding: 5px 20px; color:#000" data-id>`;

            let tr = `<tr class="car-row" onclick="carSelected(this)">`;
            table = table.concat(`${tr}${td_id}${id}</td>${td}${registration}</td>${td}${description}</td></tr>`);
        }
        $('#carsByType').empty();
        table.concat(`</tbody></table>`);
        $('#carsByType').append(`${table}`);       
    }    

    init();
}


function carSelected(e) {
    const collection = document.getElementsByClassName(e.className);
    for (i = 0; i < collection.length; i++) {
        if (collection[i].rowIndex === e.rowIndex) {
            $(collection[i]).css('background-color', '#edd4cc');
            $(collection[i]).data('selected', '1');
        }
        else {
            $(collection[i]).css('background-color', 'transparent');
            $(collection[i]).data('selected', '');
        }
    }

//    $('#book-selection').prop('disabled', false);    
}

function loadBooking() {

    // find selected row
    const collection = document.getElementsByClassName('car-row');
    let carId = 0;

    for (i = 0; i < collection.length; i++) {        
        if ($(collection[i]).data('selected') === "1") {
            carId = parseInt(collection[i].firstChild.innerHTML);
            break;
        }
    }

    if (carId === 0)
        return;

    var url = "/Home/GetCarData";
    var data = {
        carId: carId
    }

    $.ajax(
        {
            type: 'GET',
            url: url,
            dataType: 'json',
            cache: false,
            data: data,
            contentType: "application/json; charset=utf-8"
        }).fail(function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown + this.url);
        }).done(function (carData, textStatus, jqXHR) {

            // build and display html table with carList
            loadCarData(carData);
        });
}


function loadCarData(carData) {
    $('#car-category').val(carData[0].type);
    $('#car-id').val(carData[0].id);
    $('#car-mileage').val(carData[0].mileage);
    $('#car-registration').val(carData[0].registration);
}

//TODO: Implement validation
function bookCar(e) {

    e.preventDefault();

    let carId = $('#car-id').val();

    if (carId === "")
        return;

    //if ($('#carBookingForm').valid()) {

        var url = "/Home/BookCar";
        var data = {
            CarId: carId,
            Mileage: $('#car-mileage').val(),   // not really needed here
            BookingDate: $('#booking-date').val(),
            CustomerId: $('#customer-id').val(),
            BookingTime: $('#booking-time').val(),
            CarRegistration: $('#car-registration').val(),
            CarType: $('#car-category').val()
        };
     
        $.ajax(
            {
                type: 'POST',
                url: url,
                dataType: 'text',
                cache: false,
                data: data
            }).fail(function (jqXHR, textStatus, errorThrown) {
                alert(errorThrown + this.url);
            }).done(function (res, textStatus, jqXHR) {
                if (res === "0") {
                    alert('Error, booking failed');
                }
                else {
                    alert("Booking successful");
                    location.reload();  // refresh page
                }
            });
//    }
};


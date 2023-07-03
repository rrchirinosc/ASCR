function retrieveBooking() {

    let bookingId = $('#booking-id').val();

    if (bookingId === "" || bookingId === undefined)
        return;

    var url = "/Home/GetBookingData";
    var data = {
        bookingId: bookingId
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
            alert("Error or booking not found");
        }).done(function (bookingData, textStatus, jqXHR) {

            // build and display html list with booking data
            loadBookingData(bookingData);
        });
}

function loadBookingData(bookingData) {
    let list = `<ul>`;
    
    // add a row at the time as we iterate through list
    Object.entries(bookingData[0]).forEach(([key, value]) => {
        let k = key;
        let v = value;
        let li = `<li>${k}: ${v}</li>`;

        list = list.concat(`${li}`);
    })
          
    $('#existingBooking').empty();
    list.concat(`</ul>`);
    $('#existingBooking').append(`${list}`);

    //place booking id in pricing column
    $('#pricing-booking-id').val(bookingData[0].bookingId);

    // reset pricing and mileage
    $('#total-price').val("");
    $('#current-mileage').val("");
}


//TODO: Implement validation
function calculateRentalCost() {

    let bookingId = $('#pricing-booking-id').val();

    if (bookingId === "")
        return;

    var url = "/Home/CalculateBookingCost";
    var data = {
        BookingId: bookingId,
        Mileage: $('#current-mileage').val(),
        RetunDate: $('#return-time').val()        
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
        }).done(function (calculatedPrice, textStatus, jqXHR) {
            if (calculatedPrice === "-1") {
                alert('Error, cost calculation failed');
            }
            else {
                // show pricing
                $("#total-price").val(calculatedPrice);
            }
        });
};


function concludeRental() {

    let bookingId = $('#pricing-booking-id').val();

    if (bookingId === "")
        return;

    var url = "/Home/FinishBooking";
    var data = {
        BookingId: bookingId,
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
            if (res === "1") {
                alert('Rental completed, Thanks!');
                location.reload();  // refresh page
            }
            else {
                // show pricing
                $("#total-price").val(calculatedPrice);
            }
        });
};
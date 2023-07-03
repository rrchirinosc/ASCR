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
            alert(errorThrown + this.url);
        }).done(function (bookingData, textStatus, jqXHR) {

            // build and display html table with carList
            loadBookingData(bookingData);
        });
}

function loadBookingData(bookingData) {
    alert('Got booking data');
}
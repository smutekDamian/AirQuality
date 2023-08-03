function initializeAutocomplete() {
    const input = document.getElementById("autocomplete");
    const autocomplete = new google.maps.places.Autocomplete(input);
    google.maps.event.addListener(autocomplete, "place_changed", function () {
        const place = autocomplete.getPlace();
        document.getElementById("city").value = place.name;
        document.getElementById("lat").value = place.geometry.location.lat();
        document.getElementById("lng").value = place.geometry.location.lng();
    });
}

$(document).ready(function () {

    fetch('https://countriesnow.space/api/v0.1/countries/positions')
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok ' + response.statusText);
            }
            return response.json();
        })
        .then(data => {
            const sel = document.getElementById("country");
            console.log(data);
            if (!data.error) {
                for (id in data["data"]) {
                    let country_name = data["data"][id]["name"];
                    const opt = document.createElement("option");
                    opt.value = country_name;
                    opt.text = country_name;
                    opt.dataset.countryid = opt.value;

                    sel.add(opt, null);
                }
            }

        })
        .catch(error => console.error('There was a problem with the fetch operation:', error));


    $('#country').change(function () {
        let countryid = $('#country').find(":selected").data('countryid');
        let state = $("#state");
        $("#state").empty();
        console.log(countryid);
        let url = `https://countriesnow.space/api/v0.1/countries/states`;
        console.log(url);
        $.ajax({
            url: url,
            type: "POST",
            data: { "country": countryid },
            success: function (response) {
                console.log(response['data']['states'])

                if (!response.error) {

                    for (id in response['data']['states']) {
                        console.log(id)
                        let state_name = response['data']['states'][id]['name'];

                        const opt = document.createElement("option");
                        opt.value = state_name;
                        opt.text = state_name;
                        opt.dataset.stateid = state_name;

                        state.append(opt, null);
                    }
                }



            },
            error: function (request, status, error) {
                alert(request.responseText);
            }
        });
    });

    $('#state').change(function () {
        let countryid = $('#country').find(":selected").data('countryid');
        let stateid = $('#state').find(":selected").data('stateid');
        let city = $("#city");
        city.empty();
        const url = `https://countriesnow.space/api/v0.1/countries/state/cities`
        $.ajax({
            url: url,
            type: "POST",
            data: { "country": countryid, "state": stateid },
            success: function (response) {
                console.log(response['data'])

                if (!response.error) {
                    
                    for (id in response['data']) {
                        console.log(id)
                        let city_name = response['data'][id];
                        const opt = document.createElement("option");
                        opt.value = city_name;
                        opt.text = city_name;

                        city.append(opt, null);
                    }
                }



            },
            error: function (request, status, error) {
                alert(request.responseText);
            }
        });

    });



});
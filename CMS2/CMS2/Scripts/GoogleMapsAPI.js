var map, thePos, marker, icons, iconBase;
var i = 0;


var weatherMarkers = [], tempMarkers = [], hazeMarkers = [], emergencyMarkers = [];


function initMap() {

	var singapore = { lat: 1.3521, lng: 103.8198 };
	map = map = new google.maps.Map($('#map')[0], {
			zoom: 11, center: singapore
    });	


	//set icons
	icons = {
		rainy: {
			icon: icon = {
                url: '../../Content/Images/rain.png', 
				scaledSize: new google.maps.Size(50, 50), 
				origin: new google.maps.Point(0, 0), 
				anchor: new google.maps.Point(0, 0) 
			}
		},	
		sunny: {
			icon: icon = {
                url: '../../Content/Images/sunny.png',
				scaledSize: new google.maps.Size(50, 50),
				origin: new google.maps.Point(0, 0),
				anchor: new google.maps.Point(0, 0)
			}
		},	
		cloudy: {
			icon: icon = {
				url: '../../Content/Images/cloudy.png',
				scaledSize: new google.maps.Size(50, 50),
				origin: new google.maps.Point(0, 0),
				anchor: new google.maps.Point(0, 0)
			}
        },	
        lowTemp: {
            icon: icon = {
                url: '../../Content/Images/lowTemp.png',
                scaledSize: new google.maps.Size(50, 50),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(0, 0)
            }
        },	
        midTemp: {
            icon: icon = {
                url: '../../Content/Images/midTemp.png',
                scaledSize: new google.maps.Size(50, 50),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(0, 0)
            }
        },	
        highTemp: {
            icon: icon = {
                url: '../../Content/Images/highTemp.png',
                scaledSize: new google.maps.Size(50, 50),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(0, 0)
            }
        },	
        highQual: {
            icon: icon = {
                url: '../../Content/Images/highQual.png',
                scaledSize: new google.maps.Size(50, 50),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(0, 0)
            }
        },	
        midQual: {
            icon: icon = {
                url: '../../Content/Images/midQual.png',
                scaledSize: new google.maps.Size(50, 50),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(0, 0)
            }
        },	
        lowQual: {
            icon: icon = {
                url: '../../Content/Images/lowQual.png',
                scaledSize: new google.maps.Size(50, 50),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(0, 0)
            }
        },	
        fire: {
            icon: icon = {
                url: '../../Content/Images/fire.png',
                scaledSize: new google.maps.Size(50, 50),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(0, 0)
            }
        },	
        ambulence: {
            icon: icon = {
                url: '../../Content/Images/accident.png',
                scaledSize: new google.maps.Size(50, 50),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(0, 0)
            }
        },	
        gas: {
            icon: icon = {
                url: '../../Content/Images/gas.png',
                scaledSize: new google.maps.Size(50, 50),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(0, 0)
            }
        },
        evacuation: {
            icon: icon = {
                url: '../../Content/Images/evacuation.png',
                scaledSize: new google.maps.Size(50, 50),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(0, 0)
            }
        }	
	};
}

// Handle errors for locating user
function handleLocationError(browserHasGeolocation, infoWindow, pos) {
    infoWindow.setPosition(pos);
    infoWindow.setContent(browserHasGeolocation ?
        'Error: The Geolocation service failed.' :
        'Error: Your browser doesn\'t support geolocation.');
    infoWindow.open(map);
}

function pad(n) {
    return n < 10 ? '0' + n : n;
}

document.getElementById('Weather').addEventListener('change', e => {


    if (e.target.checked) {
        console.log('weather checked');

        var loc, weather, weatherIcon, start, end, lon, lat;

        var currentDate = new Date();
        var queryDateTime = currentDate.getFullYear() + '-' + pad(currentDate.getMonth() + 1) + '-' + pad(currentDate.getDate()) + 'T' + pad(currentDate.getHours()) + ':' + pad(currentDate.getMinutes()) + ':' + pad(currentDate.getSeconds());
        console.log('queryTime: ' + queryDateTime);

        fetch('https://api.data.gov.sg/v1/environment/2-hour-weather-forecast?date_time=' + queryDateTime)
            .then(function (response) {
                return response.json();
            })
            .then(function (myJson) {
                console.log(JSON.stringify(myJson));
                var infowindow = new google.maps.InfoWindow();

                start = myJson.items[0].valid_period.start.split("T")[1].split("+")[0];
                end = myJson.items[0].valid_period.end.split("T")[1].split("+")[0];
                console.log('start: ' + start);
                console.log('end ' + end);

                for (i = 0; i < myJson.area_metadata.length; i++) {
                    console.log(i);
                    loc = myJson.area_metadata[i].name;
                    console.log('location:' + loc);
                    lon = myJson.area_metadata[i].label_location.longitude;
                    console.log('longitude: ' + lon);
                    lat = myJson.area_metadata[i].label_location.latitude;
                    console.log('latitude: ' + lat);
                    weather = myJson.items[0].forecasts[i].forecast;
                    console.log('weahter: ' + weather);

                    if (weather.includes("Fair")) {
                        weatherIcon = 'sunny';
                    }
                    else if (weather.includes("Rain")) {
                        weatherIcon = 'rainy';
                    }
                    else {
                        weatherIcon = 'cloudy';
                    }
                    console.log('weather icon: ' + weatherIcon);


                    var contentString =
                        '<h1 id="heading">' + loc + '</h1>' +
                        '<div id="body">' +
                        '<p><b>Weather: </b>' + weather + '</p>' +
                        '<p><b>Time: </b>' + start + '-' + end + '</p>' +
                        '</div>';

                    var marker = new google.maps.Marker({
                        position: { lat: lat, lng: lon },
                        map: map,
                        icon: icons[weatherIcon].icon
                    });

                    google.maps.event.addListener(marker, 'click', function (contentString) {
                        return function () {
                            infowindow.setContent(contentString);
                            infowindow.open(map, this);
                        };
                    }(contentString));

                    weatherMarkers.push(marker);
                }
            });
    }
    else {
        console.log("weather unchecked");
        for (var i = 0; i < weatherMarkers.length; i++) {
            weatherMarkers[i].setMap(null);
        }
        weatherMarkers = [];
    }
});


document.getElementById('Temperature').addEventListener('change', e => {

    if (e.target.checked) {
        console.log('temperature');

        var loc, temp, tempIcon, time, lon, lat;
        var currentDate = new Date();
        var queryDateTime = currentDate.getFullYear() + '-' + pad(currentDate.getMonth() + 1) + '-' + pad(currentDate.getDate()) + 'T' + pad(currentDate.getHours()) + ':' + pad(currentDate.getMinutes()) + ':' + pad(currentDate.getSeconds());
        console.log('queryTime: ' + queryDateTime);

        fetch('https://api.data.gov.sg/v1/environment/air-temperature?date_time=' + queryDateTime)
            .then(function (response) {
                return response.json();
            })
            .then(function (myJson) {
                console.log(JSON.stringify(myJson));
                var infowindow = new google.maps.InfoWindow();

                time = myJson.items[0].timestamp.split("T")[1].split("+")[0];
                console.log('time ' + time);

                for (i = 0; i < myJson.metadata.stations.length; i++) {
                    console.log(i);
                    loc = myJson.metadata.stations[i].name;
                    console.log('location:' + loc);
                    lon = myJson.metadata.stations[i].location.longitude;
                    console.log('longitude: ' + lon);
                    lat = myJson.metadata.stations[i].location.latitude;
                    console.log('latitude: ' + lat);
                    temp = myJson.items[0].readings[i].value;

                    if (parseFloat(temp) < 20.0) {
                        tempIcon = 'lowTemp';
                    }
                    else if (parseFloat(temp) > 35.0) {
                        tempIcon = 'highTemp';
                    }
                    else {
                        tempIcon = 'midTemp';
                    }
                    console.log('temperature Icon: ' + tempIcon);

                    var contentString =
                        '<h1 id="heading">' + loc + '</h1>' +
                        '<div id="body">' +
                        '<p><b>Temperature: </b>' + temp + '&#8451</p>' +
                        '<p><b>Time: </b>' + time + '</p>' +
                        '</div>';

                    var marker = new google.maps.Marker({
                        position: { lat: lat, lng: lon },
                        map: map,
                        icon: icons[tempIcon].icon
                    });

                    google.maps.event.addListener(marker, 'click', function (contentString) {
                        return function () {
                            infowindow.setContent(contentString);
                            infowindow.open(map, this);
                        };
                    }(contentString));

                    tempMarkers.push(marker);
                }
            });
    }
    else {
        console.log("temperature unchecked");
        for (var i = 0; i < tempMarkers.length; i++) {
            tempMarkers[i].setMap(null);
        }
        tempMarkers = [];
    }
});

document.getElementById('Haze').addEventListener('change', e => {

    if (e.target.checked) {
        console.log('haze');

        var loc, qualIcon, labelColor, time, lon, lat, psi_24, pm10_24, pm10_sub, pm25_24, pm25_sub, o3_8, o3_sub, so2_24, so2_sub, co_sub, co_8, no2_1;
        var currentDate = new Date();
        var queryDateTime = currentDate.getFullYear() + '-' + pad(currentDate.getMonth() + 1) + '-' + pad(currentDate.getDate()) + 'T' + pad(currentDate.getHours()) + ':' + pad(currentDate.getMinutes()) + ':' + pad(currentDate.getSeconds());
        console.log('queryTime: ' + queryDateTime);

        fetch('https://api.data.gov.sg/v1/environment/psi?date_time=' + queryDateTime)
            .then(function (response) {
                return response.json();
            })
            .then(function (myJson) {
                console.log(JSON.stringify(myJson));
                var infowindow = new google.maps.InfoWindow();

                time = myJson.items[0].update_timestamp.split("T")[1].split("+")[0];
                console.log('time ' + time);

                for (i = 0; i < myJson.region_metadata.length; i++) {
                    console.log(i);
                    loc = myJson.region_metadata[i].name;
                    console.log('location:' + loc);
                    lon = myJson.region_metadata[i].label_location.longitude;
                    console.log('longitude: ' + lon);
                    lat = myJson.region_metadata[i].label_location.latitude;
                    console.log('latitude: ' + lat);
                    psi_24 = myJson.items[0].readings.psi_twenty_four_hourly[loc];
                    console.log('psi_24: ' + psi_24);
                    pm10_24 = myJson.items[0].readings.pm10_twenty_four_hourly[loc];
                    console.log('pm10_24: ' + pm10_24);
                    pm10_sub = myJson.items[0].readings.pm10_sub_index[loc];
                    console.log('pm10_sub: ' + pm10_sub);
                    pm25_24 = myJson.items[0].readings.pm25_twenty_four_hourly[loc];
                    console.log('pm25_24: ' + pm25_24);
                    pm25_sub = myJson.items[0].readings.pm25_sub_index[loc];
                    console.log('pm25_sub: ' + pm25_sub);
                    o3_8 = myJson.items[0].readings.o3_eight_hour_max[loc];
                    console.log('o3_8: ' + o3_8);
                    o3_sub = myJson.items[0].readings.o3_sub_index[loc];
                    console.log('o3_sub: ' + o3_sub);
                    so2_24 = myJson.items[0].readings.so2_twenty_four_hourly[loc];
                    console.log('so2_24: ' + so2_24);
                    so2_sub = myJson.items[0].readings.so2_sub_index[loc];
                    console.log('so2_sub: ' + so2_sub);
                    co_sub = myJson.items[0].readings.co_sub_index[loc];
                    console.log('co_sub: ' + co_sub);
                    co_8 = myJson.items[0].readings.co_eight_hour_max[loc];
                    console.log('co_8: ' + co_8);
                    no2_1 = myJson.items[0].readings.no2_one_hour_max[loc];
                    console.log('no2_1: ' + no2_1);


                    if (Number(psi_24) < 50) {
                        qualIcon = 'highQual';
                        labelColor = 'green';
                    }
                    else if (Number(psi_24) > 100) {
                        qualIcon = 'lowQual';
                        labelColor = 'red';
                    }
                    else {
                        qualIcon = 'midQual';
                        labelColor = 'orange';
                    }

                    var contentString =
                        '<h1 id="heading">' + loc + '</h1>' +
                        '<div id="body">' +
                        '<p><b>Update time: </b>' + time + '</p>' +
                        '<p><b>PSI 24-hour: </b>' + psi_24 + '</p>' +
                        '<p><b>PM<SUB>10</SUB> 24-hour: </b>' + pm10_24 + '</p>' +
                        '<p><b>PM<SUB>10</SUB> sub index: </b>' + pm10_24 + '</p>' +
                        '<p><b>PM<SUB>25</SUB> 24-hour: </b>' + pm25_24 + '</p>' +
                        '<p><b>PM<SUB>25</SUB> sub index: </b>' + pm25_24 + '</p>' +
                        '<p><b>O<SUB>3</SUB> 8-hour max: </b>' + o3_8 + '</p>' +
                        '<p><b>O<SUB>3</SUB> sub index: </b>' + o3_sub + '</p>' +
                        '<p><b>SO<SUB>2</SUB> 24-hour: </b>' + so2_24 + '</p>' +
                        '<p><b>SO<SUB>2</SUB> sub index: </b>' + so2_sub + '</p>' +
                        '<p><b>CO 8-hour max: </b>' + co_8 + '</p>' +
                        '<p><b>CO sub index: </b>' + co_sub + '</p>' +
                        '<p><b>NO<SUB>2</SUB> 1-hour max: </b>' + no2_1 + '</p>' +
                        '</div>';

                    var marker = new google.maps.Marker({
                        position: { lat: lat, lng: lon },
                        map: map,
                        icon: icons[qualIcon].icon,
                        label: {
                            color: labelColor,
                            fontSize: '30px',
                            text: psi_24 + ""
                        }
                    });

                    google.maps.event.addListener(marker, 'click', function (contentString) {
                        return function () {
                            infowindow.setContent(contentString);
                            infowindow.open(map, this);
                        };
                    }(contentString));

                    hazeMarkers.push(marker);
                }
            });
    }
    else {
        console.log("haze unchecked");
        for (var i = 0; i < hazeMarkers.length; i++) {
            hazeMarkers[i].setMap(null);
        }
        hazeMarkers = [];
    }
});

// Turn on/off Traffic
document.getElementById('Emergency').addEventListener('change', e => {

    var callerName, callerNumber, location, description, category, level, assistance, lon, lat, emerIcon;

    /*
     * get data from database in Json form 
     */

    emergencyData = {
        "emergencies": [
            {
                "callerName": "Wu Ziqing",
                "callerNumber": "12345678",
                "location": "Binjai Hall",
                "description": "test1",
                "category": "fire",
                "level": "1",
                "assistance": "1"
            },
            {
                "callerName": "Rabbit",
                "callerNumber": "87654321",
                "location": "NTU Hall 6",
                "description": "test2",
                "category": "ambulence",
                "level": "2",
                "assistance": "0"
            }
        ]
    };

    

    if (e.target.checked) {
        console.log('emergency');
        var infowindow = new google.maps.InfoWindow();

        async function showEmergency() {
            for (i = 0; i < emergencyData.emergencies.length; i++) {

                location = emergencyData.emergencies[i].location;
                console.log("location: " + location);
                callerName = emergencyData.emergencies[i].callerName;
                console.log("caller name: " + callerName);
                callerNumber = emergencyData.emergencies[i].callerNumber;
                console.log("caller number: " + callerNumber);
                description = emergencyData.emergencies[i].description;
                console.log("description: " + description);
                category = emergencyData.emergencies[i].category;
                console.log("category: " + category);
                level = emergencyData.emergencies[i].level;
                console.log("level: " + level);
                assistance = emergencyData.emergencies[i].assistance;
                console.log("assitance required: " + assistance);

                await fetch('https://maps.googleapis.com/maps/api/geocode/json?address=' + location + '&key=AIzaSyCxhysnIE8hbwKE-v1oEG3lGjUKFngC1SE')
                    .then(function (response) {
                        return response.json();
                    })
                    .then(function (myJson) {
                        console.log('location Json: ' + JSON.stringify(myJson));

                        lon = myJson.results[0].geometry.location.lng;
                        lat = myJson.results[0].geometry.location.lat;

                        if (category === "fire") {
                            emerIcon = 'fire';
                        }
                        else if (category === "ambulence") {
                            emerIcon = 'ambulence';
                        }
                        else if (category === "gas") {
                            emerIcon = 'gas';
                        }
                        else {
                            emerIcon = 'evacuation';
                        }
                        console.log("Icon: " + emerIcon);


                        var contentString =
                            '<h1 id="heading">' + "Title (ID? )" + '</h1>' +
                            '<div id="body">' +
                            '<p><b>Caller Name: </b>' + callerName + '</p>' +
                            '<p><b>Caller Number: </b>' + callerNumber + '</p>' +
                            '<p><b>Location: </b>' + location + '</p>' +
                            '<p><b>Description: </b>' + description + '</p>' +
                            '<p><b>Category: </b>' + category + '</p>' +
                            '<p><b>Level of Emergency: </b>' + level + '</p>' +
                            '<p><b>Assistance Required: </b>' + (assistance ? "Yes" : "No") + '</p>' +
                            '<p><b>Report time?: </b>' + '</p>' +
                            '</div>';

                        var marker = new google.maps.Marker({
                            position: { lat: lat, lng: lon },
                            map: map,
                            icon: icons[emerIcon].icon
                        });

                        google.maps.event.addListener(marker, 'click', function (contentString) {
                            return function () {
                                infowindow.setContent(contentString);
                                infowindow.open(map, this);
                            };
                        }(contentString));

                        emergencyMarkers.push(marker);
                    });
            }
        }

        showEmergency();
    }
    else {
        console.log("emergency unchecked");
        for (var i = 0; i < emergencyMarkers.length; i++) {
            emergencyMarkers[i].setMap(null);
        }
        emergencyMarkers = [];
    }
});


document.getElementById('Dengue').addEventListener('change', e => {

    var infowindow = new google.maps.InfoWindow();

    if (e.target.checked) {
        console.log("Dengue");

        map.data.setStyle({
            icon: '//example.com/path/to/image.png',
            fillColor: 'red'
        });
        map.data.loadGeoJson('../../Content/GeoJSon/dengue-clusters-geojson.json');

        map.data.addListener('click', function (event) {
            var feat = event.feature;
            var contentString = feat.getProperty('Description');
            console.log("description: " + contentString);
            infowindow.setContent(contentString);
            infowindow.setPosition(event.latLng);
            infowindow.open(map);
        });
    }

    else {
        console.log("dengue unchecked");

        var callback = function (feature) {
            map.data.remove(feature);
        };
        map.data.forEach(callback);
    }

});







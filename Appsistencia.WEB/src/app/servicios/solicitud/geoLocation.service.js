

(function () {
	'use strict';

	angular
      .module('appsistencia')
      .factory('reverseGeocoder_Factory', reverseGeocoder_Factory);

	function reverseGeocoder_Factory($http, $q) {

		var reverseGeocode = {

			googleGeocoder: function (latLng) {
				var params = latLng;
				//params.key = "AIzaSyDHYggeLr-c-5FRcqJf5XXuzS8cDb1QOxQ";
				
				var deferred = $q.defer();

				$http({
					"method": "GET",
					"url": "https://maps.googleapis.com/maps/api/geocode/json",
					"params": params
				
				})
                  .success(function (data) {
                  	console.log("%cGoogle Geocoder response: ", "color: #FFFFFF; background: #303F9F;");
                  	console.log(data);
                  	console.log("%c</>", "color: #FFFFFF; background: #303F9F;");


                  	var address = null, city = null, state = null, country = null;

                  	if (data.status !== 'ZERO_RESULTS') {

                  		var idx, levelTwo = null;

                  		var addressComponents = data.results[0].address_components;

                  		var i;

                  		for (i = 0; i < addressComponents.length; i++) {//getting country and city from google maps object

                  			if (addressComponents[i].types.indexOf("locality") !== -1) {
                  				city = addressComponents[i].long_name;
                  			}

                  			if (addressComponents[i].types.indexOf("administrative_area_level_2") !== -1) {
                  				levelTwo = addressComponents[i].long_name;
                  			}

                  			if (addressComponents[i].types.indexOf("administrative_area_level_1") !== -1) {
                  				state = addressComponents[i].long_name;
                  			}

                  			if (addressComponents[i].types.indexOf("country") !== -1) {
                  				country = addressComponents[i].short_name;
                  				break;
                  			}

                  		}

                  		if ((city === null && country === "EC") || country === "PE") {
                  			city = levelTwo;
                  		}

                  		if (state === null && country === "PE") {
                  			state = city;
                  		}

                  		address = data.results[0].formatted_address;

                  		idx = address.indexOf(',');//remove all after a ,
                  		address = (idx !== -1) ? address.slice(0, idx) : address;

                  		if (country === "CO") {//in colombia it removes all after the -
                  			idx = address.indexOf('-');
                  			address = (idx !== -1) ? address.slice(0, idx + 1) : address;
                  		}
                  	}

                  	var hasNeighborhood = data.results.map(function (result) {
                  		return result;
                  	})
					.filter(function (element) {
						if (element.types.indexOf("neighborhood") !== -1) {
							return true;
						} else {
							return false;
						}

					});

                  	// Basically if the location doesn't have a neighborhood associated, set it as undefined
                  	var nbh = hasNeighborhood.length ? hasNeighborhood[0].address_components[0].short_name : undefined;

                  	console.log("%cNeighborhood: " + nbh, "color: #FFFFFF; background: #303F9F;");

                  	deferred.resolve({ address: address, city: city, state: state, country: country, neighborhood: nbh });

                  })

                  .error(function () {

                  	deferred.reject();

                  });

				return deferred.promise

			},

			getAddress: function (lat, lng) {

				//progressIndicator_Factory.killSpinner(); //Prevention: click and drop fire this function two spinners can be created and wont be closed. This solves the issue.
				//progressIndicator_Factory.spinner();

				var deferr = $q.defer();
				var googleParams = { "latlng": lat + ',' + lng };

				this.googleGeocoder(googleParams).then(function (googleResult) {

		//			progressIndicator_Factory.killSpinner();

					if (!googleResult.city) {
						deferr.resolve({ "add": null, "city": null, "country": null });
					} else {

						if (googleResult.country === "EC") {
							//Fix: goole result for quito 
							if (googleResult.city.match(/quito/i) || googleResult.city.match(/puembo/i) || googleResult.city.match(/Cantón\s*Daule/i)) {
								googleResult.city = "Quito";
							}

							//Fix: result for guayaquil
							if (googleResult.city.match(/samborondón/i) || googleResult.city.match(/durán/i) || googleResult.city.match(/la\s*aurora/i)) {
								googleResult.city = 'Guayaquil';
							}
						}

						//PERU
						if (googleResult.country === "PE") {
							if (googleResult.city.match(/callao/i) || googleResult.city.match(/la\s*punta/i)) {
								googleResult.city = 'Lima';
							}
						}

						//Colombia
						if (googleResult.country === "CO") {
							if (googleResult.city.match(/medellín/i) || googleResult.city.match(/itagüi/i) || googleResult.city.match(/sabaneta/i) || googleResult.city.match(/envigado/i) || googleResult.city.match(/bello/i)) {
								googleResult.city = 'Medellín';
							}
							if (googleResult.city.match(/dosquebradas/i)) {
								googleResult.city = 'Pereira';
							}
						}

						deferr.resolve({ "add": googleResult.address, "state": googleResult.state, "city": googleResult.city, "country": googleResult.country, "nbh": googleResult.neighborhood });

					}
				}, function () {
			//		progressIndicator_Factory.killSpinner();
					deferr.reject({ "add": null, "state": null, "city": null, "country": null });
				});

				return deferr.promise;

			}
		};

		return reverseGeocode;

	}

})();

(function () {
	"use strict";

	angular
      .module('appsistencia')
        .service('isValidAddress_Service', isValidAddress_Service);

	//Same as android passengger app
	function isValidAddress_Service() {

		this.isValid = function (address, country) {

			if (country === "EC" || country === "PE") {
				if (address.match(/\d/) === null) {
					return false;
				}
			}

			//if the address end with -
			if (address.match(/-$/)) {
				return false;
			}

			//if the address has -
			if (address.match(/-/)) {
				var initAddress = address.slice(0, address.indexOf('-'));//si hay solo dos letras o menos antes de -
				if (initAddress.length <= 2) {
					return false;
				}
			}

			if (!address.match(/\s/g) || address.match(/\s/g).length < 2) {
				return false;
			}

			if (address.match(/^#/) || address.match(/#$/)) {
				return false;
			}

			return true;
		};

	}

})();
(function () {
	'use strict';

	angular
      .module('appsistencia')
      .factory('geocoder_Factory', geocoder_Factory);

	function geocoder_Factory($q, $http) {

		var geocode = {};

		geocode.getCoordinates = function (address) {
			var params = address;

			var deferred = $q.defer();

			$http({
				"method": "GET",
				"url": 'https://maps.googleapis.com/maps/api/geocode/json',
				"params": params

			})
              .success(function (googleData) {
              	deferred.resolve(googleData.results[0].geometry.location);
              })

			return deferred.promise;
		}

		return geocode;

	}

})();

(function () {
	"use strict";

	angular
      .module('appsistencia')
        .factory('countriesConfiguration_Factory', countriesConfiguration_Factory);

	function countriesConfiguration_Factory(SERVER_ADDRESS) {
		var countriesConfig = {};

		var urlCountries = [
          { "countryCode": "CO", "url": SERVER_ADDRESS.baseUrl, "tipInterval": 1000, "currencyCode": "COP", "callingCode": "+57" },

          { "countryCode": "EC", "url": SERVER_ADDRESS.api_ec, "tipInterval": 1, "currencyCode": "USD", "callingCode": "+593" },

          { "countryCode": "PE", "url": SERVER_ADDRESS.api_pe, "tipInterval": 1, "currencyCode": "PEN", "callingCode": "+51" }
		];

		countriesConfig.getUrl = function (countryCode) {
			if (countryCode) {
				return urlCountries
                  .filter(function (country) {
                  	return country.countryCode === countryCode.toUpperCase();
                  })
                  .map(function (country) {
                  	return country.url;
                  });
			}
		};

		countriesConfig.getTipInterval = function (countryCode) {
			if (countryCode) {
				return urlCountries
                  .filter(function (country) {
                  	return country.countryCode === countryCode.toUpperCase();
                  })
                  .map(function (country) {
                  	return country.tipInterval;
                  });
			}
		};

		countriesConfig.getCurrencyCode = function (countryCode) {
			if (countryCode) {
				return urlCountries
                  .filter(function (country) {
                  	return country.countryCode === countryCode.toUpperCase();
                  })
                  .map(function (country) {
                  	return country.currencyCode;
                  });
			}
		};

		countriesConfig.getCallingCode = function (countryCode) {
			if (countryCode) {
				return urlCountries
                  .filter(function (country) {
                  	return country.countryCode === countryCode.toUpperCase();
                  })
                  .map(function (country) {
                  	return country.callingCode;
                  });
			}
		};

		return countriesConfig;

	}

})();

(function () {
	'use strict';

	angular
      .module('appsistencia')
      .factory('cities_Factory', cities_Factory);

	function cities_Factory() {

		var cities = {};

		cities.getSupportedCities = function (cityToCheck) {
			var responseObj = {};

			var supportedCities = [
              { "city": "Armenia", "state": "Quindio", "lat": 4.53698, "lon": -75.67108, "country": "Colombia", "countryCode": "CO", "showLandmark": false },
              { "city": "Barranquilla", "state": "Atlantico", "lat": 10.9639, "lon": -74.7964, "country": "Colombia", "countryCode": "CO", "showLandmark": false },
              { "city": "Bogotá", "state": "Bogotá", "lat": 4.624335, "lon": -74.063644, "country": "Colombia", "countryCode": "CO", "showLandmark": false },
              { "city": "Bucaramanga", "state": "Santander", "lat": 7.113498, "lon": -73.1116, "country": "Colombia", "countryCode": "CO", "showLandmark": false },
              { "city": "Cali", "state": "Valle", "lat": 3.4525, "lon": -76.5358, "country": "Colombia", "countryCode": "CO", "showLandmark": false },
              { "city": "Cartagena", "state": "Bolivar", "lat": 10.4, "lon": -75.5, "country": "Colombia", "countryCode": "CO", "showLandmark": true },
              { "city": "Manizales", "state": "Caldas", "lat": 5.0659, "lon": -75.5171, "country": "Colombia", "countryCode": "CO", "showLandmark": false },
              { "city": "Medellín", "state": "Antioquia", "lat": 6.2308, "lon": -75.5906, "country": "Colombia", "countryCode": "CO", "showLandmark": true },
              { "city": "Pereira", "state": "Risaralda", "lat": 4.8143, "lon": -75.6946, "country": "Colombia", "countryCode": "CO", "showLandmark": false },
              { "city": "Guayaquil", "state": "Guayas", "lat": -2.2833, "lon": -79.8833, "country": "Ecuador", "countryCode": "EC", "showLandmark": true },
              { "city": "Quito", "state": "Pichincha", "lat": -0.2333, "lon": -78.5037, "country": "Ecuador", "countryCode": "EC", "showLandmark": true },
              { "city": "Lima", "state": "Lima", "lat": -12.0433, "lon": -77.0283, "country": "Perú", "countryCode": "PE", "showLandmark": true }
			];

			// But why here? ¯\_(ツ)_/¯
			if (!cityToCheck) {
				return supportedCities;
			}

			var city = supportedCities.filter(function (e) {
				return e.city === cityToCheck
			})

			if (city.length > 0) {
				responseObj = {
					isSupported: true,
					city: city[0]
				};
			} else {
				responseObj = {
					isSupported: false,
					city: null
				};
			}
			console.log(responseObj);
			return responseObj;


		};

		return cities;

	}

})();
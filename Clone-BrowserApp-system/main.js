var analyticsService;
var analyticsServiceTracker;

function initAnalyticsConfig(config) {
	$('#app-settings-loading').css('display', 'none');
	$('#app-settings-loaded').css('display', 'block');

	var checkbox = document.getElementById('analytics');
	checkbox.checked = config.isTrackingPermitted();
	checkbox.onchange = function() {
		config.setTrackingPermitted(checkbox.checked);
	};
}

var systemInfo = chrome.system;
var ua = window.navigator.userAgent;

var isChromeOS = false;
if(ua.indexOf('CrOS') != -1) {
/*         isChromeOS = true; */
}

var storageUnits = new Array();

function loadImage(uri, callback) {
	var xhr = new XMLHttpRequest();
	xhr.responseType = 'blob';
	xhr.onload = function() {
		callback(window.webkitURL.createObjectURL(xhr.response), uri);
	}
	xhr.open('GET', uri, true);
	xhr.send();
}

function showBounds(bounds) {
	return bounds.left + ", " + bounds.top + ", " + bounds.width + ", " + bounds.height;
}

function showInsets(bounds) {
	return bounds.left + ", " + bounds.top + ", " + bounds.right + ", " + bounds.bottom;
}

function showDisplayInfo(display) {
        var tr = '<tr>' +
                '<td>' + display.id + '</td>' +
                '<td>' + display.name + '</td>' +
                '<td class="chrome-os-only">' + display.mirroringSourceId + '</td>' +
                '<td style="text-align:center;">' + (display.isPrimary == true ? '&#10004;' : '×') + '</td>' +
                '<td style="text-align:center;">' + (display.isInternal == true ? '&#10004;' : '×') + '</td>' +
                '<td style="text-align:center;">' + (display.isEnabled == true ? '&#10004;' : '×') + '</td>' +
                '<td>' + display.dpiX + ' × ' + display.dpiY  + '</td>' +
                '<td class="chrome-os-only">' + display.rotation + '</td>' +
                '<td style="white-space: nowrap;">' + display.bounds.width + ' × ' + display.bounds.height + '</td>' +
                '<td class="chrome-os-only">' + showInsets(display.overscan) + '</td>' +
                '<td style="white-space: nowrap;">' + showBounds(display.workArea) + '</td>' +
        '</tr>';

        return tr;
}

function showStorageInfo(unit) {
        var tr = '<tr>' +
                '<td>' + unit.id + '</td>' +
                '<td>' + unit.name + '</td>' +
                '<td>' + unit.type + '</td>' +
                '<td style="text-align:right;">' + (unit.capacity != 0 ? filesize(unit.capacity) : '') + '</td>' +
        '</tr>';

        return tr;
}

function getGalleriesInfo(results) {
        if (results.length) {
                var tbody = '';
                results.forEach(function(item, indx, arr) {
                        var mData = chrome.mediaGalleries.getMediaFileSystemMetadata(item);
                        if (mData) {
                                var tr = '<tr>' +
                                        '<td>' + mData.galleryId + '</td>' +
                                        '<td>' + mData.name + '</td>' +
                                        '<td style="text-align:center;">' + (mData.isMediaDevice == true ? '&#10004;' : '×') + '</td>' +
                                        '<td style="text-align:center;">' + (mData.isRemovable == true ? '&#10004;' : '×') + '</td>' +
                                        '<td style="text-align:center;">' + (mData.isAvailable == true ? '&#10004;' : '×') + '</td>' +
                                '</tr>';

                                tbody += tr;
                        }
                });

				$('#no-media-galleries-found').css('display', 'none');
                $('#media-galleries table').css('display', 'table');
                $('#media-galleries table tbody').html(tbody);
        } else {
				$('#no-media-galleries-found').css('display', 'block');
                $('#media-galleries table').css('display', 'none');
        }
}

function showCurrentPositionWithPosition(position) {
		$('#current-location-status').css('display', 'none');
        $('#current-location-latitude').html(position.coords.latitude);
        $('#current-location-longitude').html(position.coords.longitude);

        $.getJSON('http://maps.googleapis.com/maps/api/geocode/json?latlng='+position.coords.latitude+','+position.coords.longitude+'&sensor=true', function(data) {
                if (data.status == 'OK') {
                        if (data.results && data.results.length > 0) {
                                $('#current-location-address').html(data.results[0]['formatted_address']);
                        }
                }
        });

        loadImage('http://maps.googleapis.com/maps/api/staticmap?center='+position.coords.latitude+','+position.coords.longitude+'&zoom=11&size=610x320&visual_refresh=true&markers=color:blue|label:|'+position.coords.latitude+','+position.coords.longitude+'&language='+window.navigator.language+'&sensor=true', function(blob_uri, requested_uri) {
                $('#current-location-map').attr('src', blob_uri);
        });

        $('#current-location table').css('display', 'table');
}

function showCurrentPositionWithError(error) {
        // error.code can be:
        //   0: unknown error
        //   1: permission denied
        //   2: position unavailable (error response from locaton provider)
        //   3: timed out

        $('#current-location-status').html('An error (' + error.code + ') occurred.');
		$('#current-location-status').css('display', 'block');
        $('#current-location table').css('display', 'none');
}

function getStorageInfo() {
        systemInfo.storage.getInfo(function(units) {
                storageUnits = units;
                var tbody = '';
                units.forEach(function(unit, index) {
                        tbody += showStorageInfo(unit);
                });
                $('#storage-information table tbody').html(tbody);
        });
}

function getMediaGalleries() {
        chrome.mediaGalleries.getMediaFileSystems({
                interactive : 'if_needed'
        }, getGalleriesInfo);
}

function getIdleState() {
        chrome.idle.queryState(15, function(state) {
                $('#section-machines-idle-state td').html(state)
        });
}

function getDisplayInfo() {
        systemInfo.display.getInfo(function(displays) {
                var tbody = '';
                for (var i = 0; i < displays.length; i++) {
                        tbody += showDisplayInfo(displays[i]);
                }
                $('#display-information table tbody').html(tbody);
                if (isChromeOS == false) {
                        $('#display-information table .chrome-os-only').css('display', 'none');
                }
        });
}

function getCpuInfo() {
        systemInfo.cpu.getInfo(function(cpu) {
                $('#cpu-architecture').html(cpu.archName);
                $('#cpu-model-name').html(cpu.modelName);
                $('#cpu-number-of-processors').html(cpu.numOfProcessors);
        });
}

function getMemoryInfo() {
        systemInfo.memory.getInfo(function(memory) {
                $('#memory-total-capacity').html(filesize(memory.capacity));

                var p = Math.round(100 / memory.capacity * (memory.capacity - memory.availableCapacity));
                $('#memory-used-capacity').html(filesize(memory.capacity - memory.availableCapacity) + ' (' + p+' %' + ')');

                var p = Math.round(100 / memory.capacity * memory.availableCapacity);
                $('#memory-available-capacity').html(filesize(memory.availableCapacity) + ' (' + p+' %' + ')');
        });

        setTimeout(getMemoryInfo, 1000);
}

function getOperationSystem() {
        var userAgent = window.navigator.userAgent;
        var platform = window.navigator.platform;
        var os;

        if(userAgent.indexOf('CrOS') != -1) {
                os = 'Chrome OS';
        } else if (platform === 'MacIntel' || platform === 'MacPPC' ) {
                os = 'Mac OS X ' + /10[\.\_\d]+/.exec(ua)[0];
                if (/[\_]/.test(os)) {
                        os = os.split('_').join('.');
                }
        } else if (platform === 'Win32') {
                os = 'Windows 32 bit';
        } else if (platform == 'Win64') {
                os = 'Windows 64 bit';
        } else if (!os && /Linux/.test(platform)) {
                os = 'Linux';
        } else if (!os && /Windows/.test(ua)) {
                os = 'Windows';
        }

        $('#section-operation-system td').html(os);
}

function getCurrentPosition() {
	if (navigator.geolocation) {
                $('#current-location-status').html('Geolocation is supported.<br>Determine you current location. Please wait ...');
				$('#current-location-status').css('display', 'block');
                $('#current-location table').css('display', 'none');

                navigator.geolocation.getCurrentPosition(function(position) {
                        showCurrentPositionWithPosition(position);
                }, function(error) {
                        showCurrentPositionWithError(error);
                });

                navigator.geolocation.watchPosition(function(position) {
                        showCurrentPositionWithPosition(position);
                }, function(error) {
                        showCurrentPositionWithError(error);
                });
        } else {
                $('#current-location-status').html('Geolocation is not supported.');
				$('#current-location-status').css('display', 'block');
                $('#current-location table').css('display', 'none');
        }
}

function getOnlineOrOffline() {
        if (window.navigator.onLine) {
                $('#section-online-offline td').html('online');
        } else {
                $('#section-online-offline td').html('offline');
        }
}

function getNetworkInterfaces() {
        chrome.system.network.getNetworkInterfaces(function(networkInterfaces) {
                var tbody = '';
                for (var i = 0; i < networkInterfaces.length; i++) {
                        tbody += '<tr>';
                        tbody += '<td>'+networkInterfaces[i]['name']+'</td>';
                        tbody += '<td>'+networkInterfaces[i]['address']+'</td>';
                        tbody += '<td style="text-align:right;">'+networkInterfaces[i]['prefixLength']+'</td>';
                        tbody += '</tr>';
                }
                $('#section-network table tbody').html(tbody);
        });
}

function showNotification(title, message, items) {
        var id = Math.random().toString(36).substring(7);

        if (items.length != 0) {
                items.unshift({
                        title: message,
                        message: ''
                });
        }

        chrome.notifications.create(
                id, {
                        type: (items.length == 0 ? 'basic' : 'list'),
                        iconUrl: 'Icon-48.png',
                        title: title,
                        message: (items.length == 0 ? message : ''),
                        items: items,
                        priority: 0
                },
                function() {
                        setTimeout(function() {
                                chrome.notifications.clear(
                                        id,
                                        function(wasCleared) {
                                                /* ---------- */
                                        }
                                );
                        }, 3000);
                }
        );
}

function handleOnlineOfflineChanged() {
        getOnlineOrOffline();

        if (window.navigator.onLine) {
                showNotification('System notification', 'You are online.', []);
        } else {
                showNotification('System notification', 'You are offline.', []);
        }
}

function handleDisplayChanged() {
        getDisplayInfo();
        showNotification('System notification', 'Display changed.', []);
}

function handleIdleStateChanged() {
        getIdleState();
}

function handleNewStorageAttached(storageUnitInfo) {
        getStorageInfo();
        getMediaGalleries();

        var notificationItems = new Array();
        if (storageUnitInfo.id != '') {
                notificationItems.push({
                        title: "ID",
                        message: storageUnitInfo.id
                });
        }
        if (storageUnitInfo.name != '') {
                notificationItems.push({
                        title: "Name",
                        message: storageUnitInfo.name
                });
        }
        if (storageUnitInfo.type != '') {
                notificationItems.push({
                        title: "Type",
                        message: storageUnitInfo.type
                });
        }
        if (storageUnitInfo.capacity != 0) {
                notificationItems.push({
                        title: "Total Capacity",
                        message: filesize(storageUnitInfo.capacity)
                });
        }
        showNotification('System notification', 'New storage attached.', notificationItems);
}

function handleStorageDetached(id) {
        var storageUnitInfo = new Array();
        for (var i = 0; i < storageUnits.length; i++) {
                if (storageUnits[i]['id'] == id) {
                        storageUnitInfo = storageUnits[i];
                }
        }

        getStorageInfo();
        getMediaGalleries();

        var notificationItems = new Array();
        if (storageUnitInfo.length != 0) {
                if (storageUnitInfo.id != '') {
                        notificationItems.push({
                                title: "ID",
                                message: storageUnitInfo.id
                        });
                }
                if (storageUnitInfo.name != '') {
                        notificationItems.push({
                                title: "Name",
                                message: storageUnitInfo.name
                        });
                }
                if (storageUnitInfo.type != '') {
                        notificationItems.push({
                                title: "Type",
                                message: storageUnitInfo.type
                        });
                }
                if (storageUnitInfo.capacity != 0) {
                        notificationItems.push({
                                title: "Total Capacity",
                                message: filesize(storageUnitInfo.capacity)
                        });
                }
        }
        showNotification('System notification', 'Storage detached.', notificationItems);
}

function showSection(section) {
        var sections = [
                'section-general',
                'storage-information',
                'display-information',
                'section-network',
                'media-galleries',
                'current-location',
                'section-app-settings'
        ];

        $.each(sections, function(index, value) {
                if (value == section) {
                        $("a[data-section='" + value +"']").removeClass('active').addClass('active');
						$('#'+value).css('display', 'block');
                        analyticsServiceTracker.sendAppView('View - ' + $('#'+value+' h1').html());
                } else {
                        $("a[data-section='" + value +"']").removeClass('active');
						$('#'+value).css('display', 'none');
                }
        });
}

function init() {

        analyticsService = analytics.getService('System');
        analyticsService.getConfig().addCallback(initAnalyticsConfig);

        analyticsServiceTracker = analyticsService.getTracker('UA-245330-48');

        /* ---------- */

        $('#navigation a').click(function() {
                showSection($(this).data('section'))
        });

/*
        (function saveWindowBounds() {
                 console.log(chrome.app.window.current().getBounds());
                 chrome.app.window.onBoundsChanged.addListener(saveWindowBounds);
        })();
*/

        systemInfo.storage.onAttached.addListener(function(storageUnitInfo) {
                handleNewStorageAttached(storageUnitInfo);
        });

        systemInfo.storage.onDetached.addListener(function(id) {
                handleStorageDetached(id);
        });


        chrome.idle.onStateChanged.addListener(function() {
                handleIdleStateChanged();
        });

/*
        systemInfo.display.onDisplayChanged.addListener(function() {
                handleDisplayChanged();
        });
*/

		$(window).on('offline', function() {
			handleOnlineOfflineChanged();
		});

		$(window).on('offline', function() {
			handleOnlineOfflineChanged();
		});

        getOperationSystem();
        getMemoryInfo();
        getCpuInfo();
        getDisplayInfo();
        getNetworkInterfaces();
        getIdleState();
        getMediaGalleries();
        getStorageInfo();
        getCurrentPosition();
        getOnlineOrOffline();

        showSection('section-general');

}

document.addEventListener('DOMContentLoaded', init);
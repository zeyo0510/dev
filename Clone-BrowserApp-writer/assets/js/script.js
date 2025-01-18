// Halfworks Design
// Writer - 5.3
// Author: Carlos Santos
$(document).ready(function() {
  
  $('.ql-headings').click(function(e) {
    e.stopPropagation();
    if ($('.ql-headings').hasClass('active')) {
      $(this).removeClass('active');
      $('.heading-options').hide();
    } else {
      $(this).addClass('active');
      $('.heading-options').show();
    }
  });
  $('.ql-container-toolbar span, select, #editor, nav, nav div, nav span').click(function() {
    $('.ql-headings').removeClass('active');
    $('.heading-options').hide();
  });
  $('.heading-one').click(function(e) {
    e.stopPropagation();
    e.stopImmediatePropagation();
    $('.ql-size .ql-picker-options .ql-picker-item[data-value="48px"]').click();
    $('.ql-bold').click();
  });
  $('.heading-two').click(function(e) {
    e.stopPropagation();
    e.stopImmediatePropagation();
    $('.ql-size .ql-picker-options .ql-picker-item[data-value="32px"]').click();
    $('.ql-bold').click();
  });
  $('.heading-three').click(function(e) {
    e.stopPropagation();
    e.stopImmediatePropagation();
    $('.ql-size .ql-picker-options .ql-picker-item[data-value="24px"]').click();
    $('.ql-bold').click();
  });
  $('.heading-four').click(function(e) {
    e.stopPropagation();
    e.stopImmediatePropagation();
    $('.ql-size .ql-picker-options .ql-picker-item[data-value="18px"]').click();
    $('.ql-bold').click();
  });
  //save data when editor's text changes
  $(document).on('keydown', '.ql-editor', function() {
    editorScroll();
  });
  //autoscroll
  //scrolling animation
  function editorScroll() {
    var editor = $('.ql-editor');
    var scroll = editor.scrollTop();
    var nodePos = editor.caret('position');
    editor.stop().animate({
      scrollTop: scroll + nodePos.top - editor.height() / 2 - 24
    }, 200);
  }
  $('.ql-line-spacing-list, .search-bar').hide();
  $('.modal-trigger').leanModal();
  $('.button-collapse').sideNav({
    menuWidth: 260, // Default is 240
    edge: 'left', // Choose the horizontal origin
    closeOnClick: true // Closes side-nav on <a> clicks, useful for Angular/Meteor
  });
  //dropdown
  $('.dropdown-button').dropdown({
    inDuration: 300,
    outDuration: 225,
    constrain_width: false, // Does not change width of dropdown to that of the activator
    hover: false, // Does not activate on hover
    gutter: 0, // Spacing from edge
    belowOrigin: false, // Displays dropdown below the button
    alignment: 'left' // Displays dropdown with edge aligned to the left of button
  });
  $('.dropdown-content').css('margin-top', '21px');
  $('.reset-theme').click(function() {
    $('#theme_color .modal-content .form .cyan').click();
  });
  $('.reset-accent').click(function() {
    $('#accent_color .modal-content .form .red').click();
  });
  $('.reset-font').click(function() {
    $('#roboto-font').click();
  });
  $('.reset-font-size').click(function() {
    $('#fontsize-sixteen').click();
  });
  $('.reset-margin').click(function() {
    $('#five').click();
  });
  //options
  $('#modal3 div, #modal3 input').click(function() {
    setTimeout(function() {
      saveOptions();
    }, 300);
  });
  var audioElement = document.createElement('audio');
  var vol, interval;

  function playSound(soundFile) {
    audioElement.setAttribute('src', soundFile);
    audioElement.setAttribute('loop', 'true');
    audioElement.play();
  }
  $('#modal3 .modal-content .coffee-toggle .switch label input').click(function() {
    //turned off
    if ($('#modal3 .modal-content .coffee-toggle .switch label input').attr('checked') == 'checked') {
      vol = 1;
      interval = 100;
      var fadeSound = setInterval(function() {
        if (vol > 0 && vol !== 0) {
          audioElement.volume = vol -= 0.2;
        } else {
          clearInterval(fadeSound);
          audioElement.pause();
          audioElement.currentTime = 0;
          saveOptions();
        }
      }, interval);
      $(this).removeAttr('checked');
    } else {
      vol = 0;
      interval = 100;
      var fadeInSound = setInterval(function() {
        if (vol < 1) {
          audioElement.volume = vol += 0.2;
        } else {
          clearInterval(fadeInSound);
          saveOptions();
        }
      }, interval);
      playSound('assets/sounds/CoffeeShop.mp4');
      $(this).attr('checked', 'checked');
    }
  });
  //fullscreen
  $('#modal3 .modal-content .full-toggle .switch label input').click(function() {
    if (chrome.app.window.current().isFullscreen() === true) {
      chrome.app.window.current().restore();
      $(this).removeAttr('checked');
    } else {
      chrome.app.window.current().fullscreen();
      $(this).attr('checked', 'checked');
    }
  });
  //format
  $('#modal3 .modal-content .format-toggle .switch label input').click(function() {
    //turned off
    if ($(this).attr('checked') == 'checked') {
      //if nightmode is checked
      if ($('#modal3 .modal-content .nightmode-toggle .switch label input').attr('checked') == 'checked') {
        $('#full-toolbar').slideUp(200);
        $('#full-toolbar').attr('class', 'toolbar ql-toolbar ql-snow grey darken-4');
        $('#editor').css('height', 'calc(100% - 92px)');
        $('#editor').css('margin-top', '92px');
        $(this).removeAttr('checked');
      } else {
        $('#full-toolbar').slideUp(200);
        $('#full-toolbar').attr('class', 'toolbar ql-toolbar ql-snow grey lighten-4');
        $('#editor').css('height', 'calc(100% - 92px)');
        $('#editor').css('margin-top', '92px');
        $(this).removeAttr('checked');
      }
    }
    //turned on
    else {
      //if nightmode is checked
      if ($('#modal3 .modal-content .nightmode-toggle .switch label input').attr('checked') == 'checked') {
        $('#full-toolbar').slideDown(200);
        $('#full-toolbar').attr('class', 'toolbar ql-toolbar ql-snow grey darken-4');
        $('#editor').css('height', 'calc(100% - 127px)');
        $('#editor').css('margin-top', '127px');
        $(this).attr('checked', 'checked');
      } else {
        $('#full-toolbar').slideDown(200);
        $('#full-toolbar').attr('class', 'toolbar ql-toolbar ql-snow grey lighten-4');
        $('#editor').css('height', 'calc(100% - 127px)');
        $('#editor').css('margin-top', '127px');
        $(this).attr('checked', 'checked');
      }
    }
  });
  //card
  $('#modal3 .modal-content .card-toggle .switch label input').click(function() {
    //turned off
    if ($(this).attr('checked') == 'checked') {
      //if nightmode is checked
      if ($('#modal3 .modal-content .nightmode-toggle .switch label input').attr('checked') == 'checked') {
        $('.ql-editor').removeClass('card');
        $('.ql-editor').removeClass('card-nightmode');
        $('#editor').css('background-color', '#101010');
        $(this).removeAttr('checked');
        $('#editor').css('color', '#FFF');
        $('#selector').removeAttr('class');
        $('#selector').addClass('DoesNotHaveCardHasNight');
      }
      //if nightmode is not checked
      else {
        $('.ql-editor').removeClass('card');
        $('.ql-editor').removeClass('card-nightmode');
        $('#editor').css('background-color', '#FFF');
        $(this).removeAttr('checked');
        $('#editor').css('color', '#000');
        $('#selector').removeAttr('class');
        $('#selector').addClass('DoesNotHaveCardDoesNotHaveNight');
      }
    }
    //turned on
    else {
      //if nightmode is checked
      if ($('#modal3 .modal-content .nightmode-toggle .switch label input').attr('checked') == 'checked') {
        $('.ql-editor').removeClass('card');
        $('.ql-editor').addClass('card-nightmode');
        $('#editor').css('background-color', '#101010');
        $(this).attr('checked', 'checked');
        $('#editor').css('color', '#FFF');
        $('#selector').removeAttr('class');
        $('#selector').addClass('HasCardHasNight');
      }
      //if nightmode is not checked
      else {
        $('.ql-editor').addClass('card');
        $('.ql-editor').removeClass('card-nightmode');
        $('#editor').css('background-color', '#EEE');
        $(this).attr('checked', 'checked');
        $('#editor').css('color', '#000');
        $('#selector').removeAttr('class');
        $('#selector').addClass('HasCardDoesNotHaveNight');
      }
    }
  });
  //nightmode
  $('#modal3 .modal-content .nightmode-toggle .switch label input').click(function() {
    //turned off
    if ($(this).attr('checked') == 'checked') {
      //if card is checked
      if ($('#modal3 .modal-content .card-toggle .switch label input').attr('checked') == 'checked') {
        $('.ql-editor').addClass('card');
        $('.ql-editor').removeClass('card-nightmode');
        $('#editor').css('background-color', '#EEE');
        $(this).removeAttr('checked');
        $('#editor').css('color', '#000');
        $('#selector').removeAttr('class');
        $('#selector').addClass('HasCardDoesNotHaveNight');
        if ($('#modal3 .modal-content .format-toggle .switch label input').attr('checked') == 'checked') {
          $('#full-toolbar').slideDown(200);
          $('#full-toolbar').attr('class', 'toolbar ql-toolbar ql-snow grey lighten-4');
          $('#editor').css('height', 'calc(100% - 127px)');
          $('#editor').css('margin-top', '127px');
          $('.format-toggle-input').attr('checked', 'checked');
        } else {
          $('#full-toolbar').slideUp(200);
          $('#full-toolbar').attr('class', 'toolbar ql-toolbar ql-snow grey lighten-4');
          $('#editor').css('height', 'calc(100% - 92px)');
          $('#editor').css('margin-top', '92px');
          $('.format-toggle-input').removeAttr('checked');
        }
      }
      //if card is not checked
      else {
        $('.ql-editor').removeClass('card');
        $('.ql-editor').removeClass('card-nightmode');
        $('#editor').css('background-color', '#FFF');
        $(this).removeAttr('checked');
        $('#editor').css('color', '#000');
        $('#selector').removeAttr('class');
        $('#selector').addClass('DoesNotHaveCardDoesNotHaveNight');
        if ($('#modal3 .modal-content .format-toggle .switch label input').attr('checked') == 'checked') {
          $('#full-toolbar').slideDown(200);
          $('#full-toolbar').attr('class', 'toolbar ql-toolbar ql-snow grey lighten-4');
          $('#editor').css('height', 'calc(100% - 127px)');
          $('#editor').css('margin-top', '127px');
          $('.format-toggle-input').attr('checked', 'checked');
        } else {
          $('#full-toolbar').slideUp(200);
          $('#full-toolbar').attr('class', 'toolbar ql-toolbar ql-snow grey lighten-4');
          $('#editor').css('height', 'calc(100% - 92px)');
          $('#editor').css('margin-top', '92px');
          $('.format-toggle-input').removeAttr('checked');
        }
      }
      if ($('#modal3 .modal-content .statistics-toggle .switch label input').attr('checked') == 'checked') {
        $('.statistics').slideDown(200);
        $('.statistics').attr('class', 'statistics z-depth-1 grey lighten-4');
        $('#words').attr('class', 'words grey-text text-darken-4');
        $('#characters').attr('class', 'characters grey-text text-darken-4');
        $('.statistics-toggle-input').attr('checked', 'checked');
      } else {
        $('.statistics').slideUp(200);
        $('.statistics').attr('class', 'statistics z-depth-1 grey lighten-4');
        $('#words').attr('class', 'words grey-text text-darken-4');
        $('#characters').attr('class', 'characters grey-text text-darken-4');
        $('.statistics-toggle-input').removeAttr('checked');
      }
    }
    //turned on
    else {
      //if card is checked
      if ($('#modal3 .modal-content .card-toggle .switch label input').attr('checked') == 'checked') {
        $('.ql-editor').removeClass('card');
        $('.ql-editor').addClass('card-nightmode');
        $('#editor').css('background-color', '#101010');
        $(this).attr('checked', 'checked');
        $('#editor').css('color', '#FFF');
        $('#selector').removeAttr('class');
        $('#selector').addClass('HasCardHasNight');
        if ($('#modal3 .modal-content .format-toggle .switch label input').attr('checked') == 'checked') {
          $('#full-toolbar').slideDown(200);
          $('#full-toolbar').attr('class', 'toolbar ql-toolbar ql-snow grey darken-4');
          $('#editor').css('height', 'calc(100% - 127px)');
          $('#editor').css('margin-top', '127px');
          $('.format-toggle-input').attr('checked', 'checked');
        } else {
          $('#full-toolbar').slideUp(200);
          $('#full-toolbar').attr('class', 'toolbar ql-toolbar ql-snow grey darken-4');
          $('#editor').css('height', 'calc(100% - 92px)');
          $('#editor').css('margin-top', '92px');
          $('.format-toggle-input').removeAttr('checked');
        }
      }
      //if card is not checked
      else {
        $('.ql-editor').removeClass('card');
        $('.ql-editor').removeClass('card-nightmode');
        $('#editor').css('background-color', '#101010');
        $(this).attr('checked', 'checked');
        $('#editor').css('color', '#FFF');
        $('#selector').removeAttr('class');
        $('#selector').addClass('DoesNotHaveCardHasNight');
        if ($('#modal3 .modal-content .format-toggle .switch label input').attr('checked') == 'checked') {
          $('#full-toolbar').slideDown(200);
          $('#full-toolbar').attr('class', 'toolbar ql-toolbar ql-snow grey darken-4');
          $('#editor').css('height', 'calc(100% - 127px)');
          $('#editor').css('margin-top', '127px');
          $('.format-toggle-input').attr('checked', 'checked');
        } else {
          $('#full-toolbar').slideUp(200);
          $('#full-toolbar').attr('class', 'toolbar ql-toolbar ql-snow grey darken-4');
          $('#editor').css('height', 'calc(100% - 92px)');
          $('#editor').css('margin-top', '92px');
          $('.format-toggle-input').removeAttr('checked');
        }
      }
      if ($('#modal3 .modal-content .statistics-toggle .switch label input').attr('checked') == 'checked') {
        $('.statistics').slideDown(200);
        $('.statistics').attr('class', 'statistics z-depth-1 grey darken-4');
        $('#words').attr('class', 'words grey-text text-lighten-4');
        $('#characters').attr('class', 'characters grey-text text-lighten-4');
        $('.statistics-toggle-input').attr('checked', 'checked');
      } else {
        $('.statistics').slideUp(200);
        $('.statistics').attr('class', 'statistics z-depth-1 grey darken-4');
        $('#words').attr('class', 'words grey-text text-lighten-4');
        $('#characters').attr('class', 'characters grey-text text-lighten-4');
        $('.statistics-toggle-input').removeAttr('checked');
      }
    }
  });
  //statistics
  $('#modal3 .modal-content .statistics-toggle .switch label input').click(function() {
    wordCount();
    if ($(this).attr('checked') == 'checked') {
      if ($('#modal3 .modal-content .nightmode-toggle .switch label input').attr('checked') == 'checked') {
        $('.statistics').slideUp(200);
        $('.statistics').attr('statistics z-depth-1 grey darken-4');
        $(this).removeAttr('checked');
      } else {
        $('.statistics').slideUp(200);
        $('.statistics').attr('statistics z-depth-1 grey lighten-4');
        $(this).removeAttr('checked');
      }
    } else {
      if ($('#modal3 .modal-content .nightmode-toggle .switch label input').attr('checked') == 'checked') {
        $('.statistics').slideDown(200);
        $('.statistics').attr('statistics z-depth-1 grey darken-4');
        $(this).attr('checked', 'checked');
      } else {
        $('.statistics').slideDown(200);
        $('.statistics').attr('statistics z-depth-1 grey lighten-4');
        $(this).attr('checked', 'checked');
      }
    }
  });
  //welcome menu toggle
  $('#modal3 .modal-content .welcome-toggle .switch label input').click(function() {
    //turned off
    if ($(this).attr('checked') == 'checked') {
      $('#welcome').attr('class', 'modal welcome bottom-sheet hide');
      $(this).removeAttr('checked');
    }
    //turned on
    else {
      $('#welcome').attr('class', 'modal welcome bottom-sheet show');
      $(this).attr('checked', 'checked');
    }
  });
  //margin-size
  var margin;
  $('#zero').click(function() {
    margin = 0;
    $('#editor').css('padding-left', '0%');
    $('#editor').css('padding-right', '0%');
    saveOptions();
    editor.focus();
  });
  $('#five').click(function() {
    margin = 5;
    $('#editor').css('padding-left', '5%');
    $('#editor').css('padding-right', '5%');
    saveOptions();
    editor.focus();
  });
  $('#ten').click(function() {
    margin = 10;
    $('#editor').css('padding-left', '10%');
    $('#editor').css('padding-right', '10%');
    saveOptions();
    editor.focus();
  });
  $('#fifteen').click(function() {
    margin = 15;
    $('#editor').css('padding-left', '15%');
    $('#editor').css('padding-right', '15%');
    saveOptions();
    editor.focus();
  });
  $('#twenty').click(function() {
    margin = 20;
    $('#editor').css('padding-left', '20%');
    $('#editor').css('padding-right', '20%');
    saveOptions();
    editor.focus();
  });
  $('#twenty-five').click(function() {
    margin = 25;
    $('#editor').css('padding-left', '25%');
    $('#editor').css('padding-right', '25%');
    saveOptions();
    editor.focus();
  });
  //default file type
  //wtr
  $(document).on('click', '#wtr', function() {
    var file = $('.doc-name').text();
    var fileName = file.substring(file.lastIndexOf('Writer - ') + 1, file.lastIndexOf('.'));
    $('#txt').removeAttr('checked');
    $('#wtr').attr('checked', 'checked');
    $('#html').removeAttr('checked');
    $('#md').removeAttr('checked');
    $('.format-toggle .switch input').removeAttr('disabled');
    $('.format-toggle .switch input').attr('checked', 'checked');
    $('#full-toolbar').slideDown(200);
    $('#editor').css('height', 'calc(100% - 127px)');
    $('#editor').css('margin-top', '127px');
    $('.doc-name').text(fileName + '.wtr');
    $('.brand-logo').text('Writer - ' + fileName + '.wtr');
    $('.doc-type').text('wtr');
    $('.doc-size').text('0');
    $('#file_selector').attr('class', 'isWTR');
    savedFileEntry = null;
    chrome.storage.local.set({
      documententry: savedFileEntry
    });
    saveFileName();
    saveOptions();
  });
  //html
  $(document).on('click', '#html', function() {
    var file = $('.doc-name').text();
    var fileName = file.substring(file.lastIndexOf('Writer - ') + 1, file.lastIndexOf('.'));
    $('#txt').removeAttr('checked');
    $('#html').attr('checked', 'checked');
    $('#wtr').removeAttr('checked');
    $('#md').removeAttr('checked');
    $('.format-toggle .switch input').removeAttr('disabled');
    $('.format-toggle .switch input').attr('checked', 'checked');
    $('#full-toolbar').slideDown(200);
    $('#editor').css('height', 'calc(100% - 127px)');
    $('#editor').css('margin-top', '127px');
    $('.doc-name').text(fileName + '.html');
    $('.brand-logo').text('Writer - ' + fileName + '.html');
    $('.doc-type').text('html');
    $('.doc-size').text('0');
    $('#file_selector').attr('class', 'isHTML');
    savedFileEntry = null;
    chrome.storage.local.set({
      documententry: savedFileEntry
    });
    saveFileName();
    saveOptions();
  });
  //md
  $(document).on('click', '#md', function() {
    var file = $('.doc-name').text();
    var fileName = file.substring(file.lastIndexOf('Writer - ') + 1, file.lastIndexOf('.'));
    $('#txt').removeAttr('checked');
    $('#md').attr('checked', 'checked');
    $('#wtr').removeAttr('checked');
    $('#html').removeAttr('checked', 'checked');
    $('.format-toggle .switch input').removeAttr('disabled');
    $('.format-toggle .switch input').attr('checked', 'checked');
    $('#full-toolbar').slideDown(200);
    $('#editor').css('height', 'calc(100% - 127px)');
    $('#editor').css('margin-top', '127px');
    $('.doc-name').text(fileName + '.md');
    $('.brand-logo').text('Writer - ' + fileName + '.md');
    $('.doc-type').text('md');
    $('.doc-size').text('0');
    $('#file_selector').attr('class', 'isMD');
    savedFileEntry = null;
    chrome.storage.local.set({
      documententry: savedFileEntry
    });
    saveFileName();
    saveOptions();
  });
  //text
  $(document).on('click', '#txt', function() {
    var file = $('.doc-name').text();
    var fileName = file.substring(file.lastIndexOf('Writer - ') + 1, file.lastIndexOf('.'));
    var editorText = editor.getText();
    $('#wtr').removeAttr('checked');
    $('#md').removeAttr('checked');
    $('#html').removeAttr('checked');
    $('#txt').attr('checked', 'checked');
    $('.format-toggle .switch input').removeAttr('checked');
    $('.format-toggle .switch input').attr('disabled', 'disabled');
    $('#full-toolbar').slideUp(200);
    $('#editor').css('height', 'calc(100% - 92px)');
    $('#editor').css('margin-top', '92px');
    $('.doc-name').text(fileName + '.txt');
    $('.brand-logo').text('Writer - ' + fileName + '.txt');
    $('.doc-type').text('txt');
    $('.doc-size').text('0');
    $('#file_selector').attr('class', 'isTXT');
    savedFileEntry = null;
    chrome.storage.local.set({
      documententry: savedFileEntry
    });
    saveFileName();
    saveOptions();
  });
  //default font size
  $('#default_font_size .modal-content p').click(function() {
    saveOptions();
  });
  $('#fontsize-ten').click(function() {
    $('.ql-size option').removeAttr('selected');
    $('.ten-option').attr('selected', '');
    $('.ql-size .ql-picker-options .ql-picker-item').removeClass('ql-selected');
    $('.ql-size .ql-picker-options .ql-picker-item[data-value="10px"]').addClass('ql-selected').click();
    $('#editor').css('font-size', '10px');
  });
  $('#fontsize-twelve').click(function() {
    $('.ql-size option').removeAttr('selected');
    $('.twelve-option').attr('selected', '');
    $('.ql-size .ql-picker-options .ql-picker-item').removeClass('ql-selected');
    $('.ql-size .ql-picker-options .ql-picker-item[data-value="12px"]').addClass('ql-selected').click();
    $('#editor').css('font-size', '12px');
  });
  $('#fontsize-sixteen').click(function() {
    $('.ql-size option').removeAttr('selected');
    $('.sixteen-option').attr('selected', '');
    $('.ql-size .ql-picker-options .ql-picker-item').removeClass('ql-selected');
    $('.ql-size .ql-picker-options .ql-picker-item[data-value="16px"]').addClass('ql-selected').click();
    $('#editor').css('font-size', '16px');
  });
  $('#fontsize-eighteen').click(function() {
    $('.ql-size option').removeAttr('selected');
    $('.eighteen-option').attr('selected', '');
    $('.ql-size .ql-picker-options .ql-picker-item').removeClass('ql-selected');
    $('.ql-size .ql-picker-options .ql-picker-item[data-value="18px"]').addClass('ql-selected').click();
    $('#editor').css('font-size', '18px');
  });
  $('#fontsize-twentyfour').click(function() {
    $('.ql-size option').removeAttr('selected');
    $('.twentyfour-option').attr('selected', '');
    $('.ql-size .ql-picker-options .ql-picker-item').removeClass('ql-selected');
    $('.ql-size .ql-picker-options .ql-picker-item[data-value="24px"]').addClass('ql-selected').click();
    $('#editor').css('font-size', '24px');
  });
  $('#fontsize-thirtytwo').click(function() {
    $('.ql-size option').removeAttr('selected');
    $('.thirtytwo-option').attr('selected', '');
    $('.ql-size .ql-picker-options .ql-picker-item').removeClass('ql-selected');
    $('.ql-size .ql-picker-options .ql-picker-item[data-value="32px"]').addClass('ql-selected').click();
    $('#editor').css('font-size', '32px');
  });
  $('#fontsize-fourtyeight').click(function() {
    $('.ql-size option').removeAttr('selected');
    $('.fourtyeight-option').attr('selected', '');
    $('.ql-size .ql-picker-options .ql-picker-item').removeClass('ql-selected');
    $('.ql-size .ql-picker-options .ql-picker-item[data-value="48px"]').addClass('ql-selected').click();
    $('#editor').css('font-size', '48px');
  });
  //default font types
  $('#default_font .modal-content p').click(function() {
    saveOptions();
  });
  $('#arial-font').click(function() {
    $('.ql-font option').removeAttr('selected');
    $('.arial-option').attr('selected', '');
    $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
    $('.ql-font .ql-picker-options .ql-picker-item[data-value="Arial"]').addClass('ql-selected').click();
    $('#editor').css('font-family', 'Arial');
  });
  $('#calibri-font').click(function() {
    $('.ql-font option').removeAttr('selected');
    $('.calibri-option').attr('selected', '');
    $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
    $('.ql-font .ql-picker-options .ql-picker-item[data-value="Calibri"]').addClass('ql-selected').click();
    $('#editor').css('font-family', 'Calibri');
  });
  $('#comic-sans-font').click(function() {
    $('.ql-font option').removeAttr('selected');
    $('.comic-sans-option').attr('selected', '');
    $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
    $('.ql-font .ql-picker-options .ql-picker-item[data-value="Comic Sans MS"]').addClass('ql-selected').click();
    $('#editor').css('font-family', 'Comic Sans MS');
  });
  $('#courier-font').click(function() {
    $('.ql-font option').removeAttr('selected');
    $('.courier-option').attr('selected', '');
    $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
    $('.ql-font .ql-picker-options .ql-picker-item[data-value="Courier"]').addClass('ql-selected').click();
    $('#editor').css('font-family', 'Courier');
  });
  $('#georgia-font').click(function() {
    $('.ql-font option').removeAttr('selected');
    $('.georgia-option').attr('selected', '');
    $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
    $('.ql-font .ql-picker-options .ql-picker-item[data-value="Georgia"]').addClass('ql-selected').click();
    $('#editor').css('font-family', 'Georgia');
  });
  $('#helvetica-font').click(function() {
    $('.ql-font option').removeAttr('selected');
    $('.helvetica-option').attr('selected', '');
    $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
    $('.ql-font .ql-picker-options .ql-picker-item[data-value="Helvetica"]').addClass('ql-selected').click();
    $('#editor').css('font-family', 'Helvetica');
  });
  $('#open-sans-font').click(function() {
    $('.ql-font option').removeAttr('selected');
    $('.open-sans-option').attr('selected', '');
    $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
    $('.ql-font .ql-picker-options .ql-picker-item[data-value="Open Sans"]').addClass('ql-selected').click();
    $('#editor').css('font-family', 'Open Sans');
  });
  $('#palatino-font').click(function() {
    $('.ql-font option').removeAttr('selected');
    $('.palatino-option').attr('selected', '');
    $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
    $('.ql-font .ql-picker-options .ql-picker-item[data-value="Palatino"]').addClass('ql-selected').click();
    $('#editor').css('font-family', 'Palatino');
  });
  $('#trebuchet-font').click(function() {
    $('.ql-font option').removeAttr('selected');
    $('.trebuchet-option').attr('selected', '');
    $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
    $('.ql-font .ql-picker-options .ql-picker-item[data-value="Trebuchet MS"]').addClass('ql-selected').click();
    $('#editor').css('font-family', 'Trebuchet MS');
  });
  $('#sans-serif-font').click(function() {
    $('.ql-font option').removeAttr('selected');
    $('.sans-serif-option').attr('selected', '');
    $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
    $('.ql-font .ql-picker-options .ql-picker-item[data-value="sans-serif"]').addClass('ql-selected').click();
    $('#editor').css('font-family', 'sans-serif');
  });
  $('#serif-font').click(function() {
    $('.ql-font option').removeAttr('selected');
    $('.serif-option').attr('selected', '');
    $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
    $('.ql-font .ql-picker-options .ql-picker-item[data-value="serif"]').addClass('ql-selected').click();
    $('#editor').css('font-family', 'serif');
  });
  $('#roboto-font').click(function() {
    $('.ql-font option').removeAttr('selected');
    $('.roboto-option').attr('selected', '');
    $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
    $('.ql-font .ql-picker-options .ql-picker-item[data-value="Roboto"]').addClass('ql-selected').click();
    $('#editor').css('font-family', 'Roboto');
  });
  $('#monospace-font').click(function() {
    $('.ql-font option').removeAttr('selected');
    $('.monospace-option').attr('selected', '');
    $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
    $('.ql-font .ql-picker-options .ql-picker-item[data-value="monospace"]').addClass('ql-selected').click();
    $('#editor').css('font-family', 'monospace');
  });
  $('#times-font').click(function() {
    $('.ql-font option').removeAttr('selected');
    $('.times-option').attr('selected', '');
    $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
    $('.ql-font .ql-picker-options .ql-picker-item[data-value="Times New Roman"]').addClass('ql-selected').click();
    $('#editor').css('font-family', 'Times New Roman');
  });
  $('#verdana-font').click(function() {
    $('.ql-font option').removeAttr('selected');
    $('.verdana-option').attr('selected', '');
    $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
    $('.ql-font .ql-picker-options .ql-picker-item[data-value="Verdana"]').addClass('ql-selected').click();
    $('#editor').css('font-family', 'Verdana');
  });
  //themes
  function windowColor() {
    $('.appnav').css('background-color', $('nav').css('background-color'));
  }
  $('#theme_color .modal-content .form div').click(function() {
    var color = $(this).css('background-color');
    $('.nav-wrapper').css('background-color', color);
    if ($(this).hasClass('nav-dark')) {
      $('nav .button-collapse i, .brand-logo, .nav-wrapper .material-icons, .search-field').css('color', 'rgba(0,0,0,0.87)');
      $('.line').css('background-color', 'rgba(0,0,0,0.87)');
    } else {
      $('nav .button-collapse i, .brand-logo, .nav-wrapper .material-icons, .search-field').css('color', 'rgba(255,255,255,0.87)');
      $('.line').css('background-color', 'rgba(255,255,255,0.87)');
    }
    windowColor();
  });
  //accents
  $('#accent_color .modal-content .form div').click(function() {
    $('.btn-floating').attr('class', 'btn-floating btn-large right waves-effect waves-light modal-trigger');
    var previousClass = $('.selected').attr('class');
    $('.selected').removeClass('selected');
    var color = $(this).attr('class');
    $(this).addClass('selected');
    $('.btn-floating').removeClass(previousClass);
    $('.btn-floating').addClass(color);
    if ($(this).hasClass('fab-dark')) {
      $('.btn-floating i').css('color', 'rgba(0,0,0,0.87)');
    } else {
      $('.btn-floating i').css('color', 'rgba(255,255,255,0.87)');
    }
  });
  //quill editor stuff
  //initialize quill
  var editor = new Quill('#editor', {
    formats: ['bold', 'italic', 'strike', 'underline', 'font', 'size', 'color', 'background', 'link', 'bullet', 'list', 'align'],
    theme: 'snow'
  });
  editor.addModule('toolbar', {
    container: '#full-toolbar'
  });
  editor.addModule('link-tooltip', true);
  $('.ql-editor').addClass('card');
  //set lean-overlay to z-index 998
  $('.materialize-lean-overlay-15, .materialize-lean-overlay-16').css('z-index', '998');
  //line-spacing
  $('.ql-line-spacing').click(function() {
    if ($('.ql-line-spacing-list').is(':visible')) {
      $('.ql-line-spacing-list').hide();
    } else {
      $('.ql-line-spacing-list').show();
    }
  });
  $('.navbar-fixed, #editor, .fixed-action-btn, .ql-bold, .ql-italic, .ql-strike, .ql-underline, .ql-font, .ql-size, .ql-color, .ql-background, .ql-link, .ql-bullet, .ql-list, .ql-align, .ql-picker-label').click(function() {
    $('.ql-line-spacing-list').hide();
  });
  //search
  $('.search-button').click(function() {
    var range = editor.getSelection();
    $('.search-bar').show().animate({
      opacity: '1'
    }, 200);
    if (range) {
      var text = editor.getText(range.start, range.end);
      $('.search-field').focus();
      $('.search-field').val(text);
    } else {
      $('.search-field').focus();
      $('.search-field').val('');
    }
  });
  $('.back').click(function() {
    $('.search-bar').animate({
      opacity: '0'
    }, 200, function() {
      $(this).hide();
    });
    editor.focus();
    $('.search-field').val('');
    $('.ql-editor div').unhighlight();
    $('.search-bar').css('outline', 'none');
  });
  (function($) {
    $.fn.goTo = function() {
      $('.ql-editor').animate({
        scrollTop: $(this).offset().top - $('.ql-editor').offset().top + $('.ql-editor').scrollTop() - 20
      }, 200);
      return this;
    };
  })(jQuery);
  $('.search-field').keyup(function(e) {
    if ($('.ql-editor').text().toLowerCase().indexOf($(this).val().toLowerCase()) > -1) {
      $('.ql-editor div').unhighlight();
      $('.ql-editor div').highlight($(this).val(), {
        caseSensitive: false
      });
      $('.search-bar').css('outline', 'none');
      $('.highlight').goTo();
    } else {
      $('.ql-editor div').unhighlight();
      $('.search-bar').css('outline', '1px solid ' + $('.btn-floating').css('background-color'));
    }
  });
  $('#editor, .btn-floating').click(function() {
    $('.search-bar').animate({
      opacity: '0'
    }, 200, function() {
      $(this).hide();
    });
    $('.search-field').val('');
    $('.ql-editor div').unhighlight();
    $('.search-bar').css('outline', 'none');
  });
  $('.search-field').click(function() {
    if ($('.ql-editor').text().toLowerCase().indexOf($(this).val().toLowerCase()) > -1) {
      $('.ql-editor div').unhighlight();
      $('.ql-editor div').highlight($(this).val(), {
        caseSensitive: false
      });
      $('.search-bar').css('outline', 'none');
      $('.highlight').goTo();
    } else {
      $('.ql-editor div').unhighlight();
      $('.search-bar').css('outline', '1px solid ' + $('.btn-floating').css('background-color'));
    }
  });

  function lineSpacingSave() {
    var lineSpacing = $('.ql-editor').css('line-height');
    chrome.storage.local.set({
      linespacing: lineSpacing
    });
  }

  function lineSpacingLoad() {
    chrome.storage.local.get({
      linespacing: 'lineSpacing'
    }, function(line) {
      var linespacing = line.linespacing;
      if (linespacing == 'lineSpacing') {
        $('.ql-editor').css('line-height', '1.5');
        $('.one-half-line').attr('class', 'selected');
      } else {
        $('.ql-editor').css('line-height', linespacing);
        if (linespacing == '16px') {
          $('.ql-line-spacing-list li').removeClass('selected');
          $('.single-line').attr('class', 'selected');
        }
        if (linespacing == '18.4px') {
          $('.ql-line-spacing-list li').removeClass('selected');
          $('.one-fifteen-line').attr('class', 'selected');
        }
        if (linespacing == '24px') {
          $('.ql-line-spacing-list li').removeClass('selected');
          $('.one-half-line').attr('class', 'selected');
        }
        if (linespacing == '32px') {
          $('.ql-line-spacing-list li').removeClass('selected');
          $('.double-line').attr('class', 'selected');
        }
      }
    });
  }
  $('.ql-line-spacing-list li').click(function() {
    $('.ql-line-spacing-list').hide();
    if ($(this).text() == 'Single' || $(this).text() == 'Double') {
      if ($(this).text() == 'Single') {
        $('.ql-editor').css('line-height', '1.0');
        lineSpacingSave();
      } else {
        $('.ql-editor').css('line-height', '2.0');
        lineSpacingSave();
      }
    } else {
      $('.ql-editor').css('line-height', $(this).text());
      lineSpacingSave();
    }
    $('.ql-line-spacing-list li').removeClass('selected');
    $(this).addClass('selected');
    editor.focus();
  });
  //spellcheck
  function spellSave() {
    var spellCheck = $('#editor').attr('spellcheck');
    chrome.storage.local.set({
      spellcheck: spellCheck
    });
  }

  function spellLoad() {
    chrome.storage.local.get({
      spellcheck: 'spellCheck'
    }, function(spell) {
      var spellcheck = spell.spellcheck;
      if (spellcheck == 'spellCheck') {
        $('.ql-editor div, #editor, .ql-editor').attr('spellcheck', false);
        $('.ql-spellcheck').removeClass('selected');
      }
      if (spellcheck == 'true') {
        $('.ql-editor div, #editor, .ql-editor').attr('spellcheck', true);
        $('.ql-spellcheck').addClass('selected');
      }
      if (spellcheck == 'false') {
        $('.ql-editor div, #editor, .ql-editor').attr('spellcheck', false);
        $('.ql-spellcheck').removeClass('selected');
      }
    });
  }
  $('.ql-spellcheck').click(function() {
    if ($('.ql-editor div').attr('spellcheck') == 'true') {
      $('.ql-editor div, #editor, .ql-editor').attr('spellcheck', false);
      $('.ql-spellcheck').removeClass('selected');
      spellSave();
    } else {
      $('.ql-editor div, #editor, .ql-editor').attr('spellcheck', true);
      $('.ql-spellcheck').addClass('selected');
      spellSave();
    }
  });
  //clear
  function clear() {
    editor.setHTML('');
  }
  //save
  function save() {
    var savedText = editor.getHTML();
    chrome.storage.local.set({
      text: savedText
    });
  }
  //load
  function load() {
    var savedText = editor.getHTML();
    chrome.storage.local.get({
      text: 'savedText'
    }, function(item) {
      var text = item.text;
      if (text == 'savedText') {
        editor.setHTML('');
      } else {
        $('.ql-editor').html(text);
      }
    });
  }

  function fileSize() {
    var blob = new Blob([ $('.ql-editor').html() ]);
    var size = blob.size;
    $('.doc-size').text(Math.round(size / 1000) + ' KB');
  }

  function wordCount() {
    var txt = editor.getText();
    var lineheight = parseInt($('.ql-editor').css('line-height'));
    var scrollHeight = parseInt($('.ql-editor').css('height'));
    if ($('.ql-editor').text() === '') {
      $('.words').text('0');
      $('.characters').text('0');
      $('.characters-exc').text('0');
      $('.paragraphs').text('0');
    } else {
      var wordcount = txt.split(/\S+/g).length - 1;
      var charCount = txt.length - 1;
      var charExcCount = txt.replace(/ /g, '').length - 1;
      var paragraphs = txt.replace(/\n$/gm, '').split(/\n/).length;
      $('.words').text(wordcount);
      $('.characters').text(charCount);
      $('.characters-exc').text(charExcCount);
      $('.paragraphs').text(paragraphs);
    }
  }
  var saved;
  var timer;
  editor.on('text-change', function() {
    if ($('.brand-logo').text().indexOf('*') > -1 || saved == 1) {
      saved = 0;
    } else {
      $('.brand-logo').text($('.brand-logo').text() + '*');
      setTimeout(function() {
        saveFileName();
      }, 200);
    }
    clearTimeout(timer);
    timer = setTimeout(function() {
      if (savedFileEntry) {
        exportToFileEntry(savedFileEntry);
      }
      save();
    }, 400);
    $('.ql-line-spacing-list').hide();
    if ($('.statistics-toggle-input').attr('checked') == 'checked') {
      wordCount();
    } else {}
  });
  $('.word-count').click(function() {
    wordCount();
  });
  $('.details').click(function() {
    var txt = editor.getText();
    var wordcount = txt.split(/\S+/g).length - 1;
    var docName = $('.doc-name').text();
    var docType = docName.split('.').pop();
    var docTime = Math.round(wordcount / 200) + ' Min';
    if (docName.indexOf('.') > -1) {
      $('.doc-type').text(docType);
    } else {
      $('.doc-type').text('File');
    }
    $('.doc-time').text(docTime);
    fileSize();
  });
  //create new document
  $('.create').click(function() {
    setTimeout(function() {
      clear();
      var isSuccess = $('.doc-name').text().indexOf('.wtr') > -1;
      if (isSuccess) {
        $('.doc-name').text('Untitled.wtr');
        $('.brand-logo').text('Writer - Untitled.wtr');
        $('.doc-type').text('wtr');
        $('.doc-size').text('0');
        fileSize();
        saveFileName();
        saveOptions();
      } else if (!isSuccess && $('.doc-name').text().indexOf('.html') > -1) {
        $('.doc-name').text('Untitled.html');
        $('.brand-logo').text('Writer - Untitled.html');
        $('.doc-type').text('html');
        $('.doc-size').text('0');
        fileSize();
        saveFileName();
        saveOptions();
      } else if (!isSuccess && $('.doc-name').text().indexOf('.md') > -1) {
        $('.doc-name').text('Untitled.md');
        $('.brand-logo').text('Writer - Untitled.md');
        $('.doc-type').text('md');
        $('.doc-size').text('0');
        fileSize();
        saveFileName();
        saveOptions();
      } else if (!isSuccess && $('.doc-name').text().indexOf('.txt') > -1) {
        $('.doc-name').text('Untitled.txt');
        $('.brand-logo').text('Writer - Untitled.txt');
        $('.doc-type').text('txt');
        $('.doc-size').text('0');
        fileSize();
        saveFileName();
        saveOptions();
      } else if (!isSuccess) {
        $('.doc-name').text('Untitled.txt');
        $('.brand-logo').text('Writer - Untitled.txt');
        $('.doc-type').text('txt');
        $('.doc-size').text('0');
        fileSize();
        saveFileName();
        saveOptions();
      }
      save();
    }, 300);
    $('#modal1').closeModal();
    savedFileEntry = null;
    chrome.storage.local.set({
      documententry: savedFileEntry
    });
  });
  //alternative
  $('.new').click(function() {
    $('#modal1').openModal();
  });
  //save document
  //save file name
  function saveFileName() {
    var fileName = $('.doc-name').text();
    var title = $('.brand-logo').text();
    chrome.storage.local.set({
      filename: fileName,
      titleSaved: title
    });
  }
  //load file name
  function loadFileName() {
    chrome.storage.local.get({
      filename: 'fileName',
      titleSaved: 'title'
    }, function(name) {
      var filename = name.filename;
      var titleSaved = name.titleSaved;
      if (filename == 'fileName') {
        $('.doc-name').text('Untitled.wtr');
      } else {
        $('.doc-name').text(filename);
      }
      if (titleSaved == 'title') {
        $('.brand-logo').text('Writer - Untitled.wtr');
      } else {
        $('.brand-logo').text(titleSaved);
      }
    });
  }
  var dbName = 'text-vanillajs';
  var savedFileEntry, fileDisplayPath;
  var documentEntry;

  function getTextAsText(callback) {
    chrome.storage.local.get(dbName, function(storedData) {
      var text = editor.getText();
      callback(text);
    }.bind(this));
  }

  function getTextAsHtml(callback) {
    chrome.storage.local.get(dbName, function(storedData) {
      var text = editor.getHTML();
      callback(text);
    }.bind(this));
  }

  function exportToFileEntry(fileEntry) {
    savedFileEntry = fileEntry;
    documentEntry = chrome.fileSystem.retainEntry(fileEntry);
    chrome.storage.local.set({
      documententry: documentEntry
    });
    $('.ql-editor div').unhighlight();
    if ($('#wtr').attr('checked') == 'checked') {
      getTextAsHtml(function(contents) {
        fileEntry.createWriter(function(fileWriter) {
          var truncated = false;
          var blob = new Blob([contents]);
          fileWriter.onwriteend = function(e) {
            if (!truncated) {
              truncated = true;
              // You need to explicitly set the file size to truncate
              // any content that might have been there before
              this.truncate(blob.size);
              return;
            }
          };
          fileWriter.write(blob);
          $('.doc-name').text(fileEntry.name);
          $('.brand-logo').text('Writer - ' + fileEntry.name);
          saveFileName();
        });
      });
    }
    if ($('#html').attr('checked') == 'checked') {
      getTextAsHtml(function(contents) {
        fileEntry.createWriter(function(fileWriter) {
          var truncated = false;
          var contentHTML = '<!DOCTYPE HTML/><html><body>' + contents + '</body></html>';
          var blob = new Blob([contentHTML]);
          fileWriter.onwriteend = function(e) {
            if (!truncated) {
              truncated = true;
              // You need to explicitly set the file size to truncate
              // any content that might have been there before
              this.truncate(blob.size);
              return;
            }
          };
          fileWriter.write(blob);
          $('.doc-name').text(fileEntry.name);
          $('.brand-logo').text('Writer - ' + fileEntry.name);
          saveFileName();
        });
      });
    }
    if ($('#md').attr('checked') == 'checked') {
      getTextAsHtml(function(contents) {
        fileEntry.createWriter(function(fileWriter) {
          var truncated = false;
          contents = contents.replace(/<div/ig, "<p");
          contents = contents.replace(/<\/div>/ig, "</p>");
          var blob = new Blob([toMarkdown(contents)], {
            type: 'text/markdown'
          });
          fileWriter.onwriteend = function(e) {
            if (!truncated) {
              truncated = true;
              // You need to explicitly set the file size to truncate
              // any content that might have been there before
              this.truncate(blob.size);
              return;
            }
          };
          fileWriter.write(blob);
          $('.doc-name').text(fileEntry.name);
          $('.brand-logo').text('Writer - ' + fileEntry.name);
          saveFileName();
        });
      });
    }
    if ($('#txt').attr('checked') == 'checked') {
      getTextAsText(function(contents) {
        fileEntry.createWriter(function(fileWriters) {
          var truncated = false;
          var blob = new Blob([contents]);
          fileWriters.onwriteend = function(e) {
            if (!truncated) {
              truncated = true;
              // You need to explicitly set the file size to truncate
              // any content that might have been there before
              this.truncate(blob.size);
              return;
            }
          };
          fileWriters.write(blob);
          $('.doc-name').text(fileEntry.name);
          $('.brand-logo').text('Writer - ' + fileEntry.name);
          saveFileName();
        });
      });
    }
  }

  function ExportToDisk() {
    var file = $('.doc-name').text();
    var fileName = file.split('.').pop();
    $('.ql-editor div').unhighlight();
    if (fileName == 'wtr') {
      chrome.fileSystem.chooseEntry({
        type: 'saveFile',
        suggestedName: file,
        accepts: [{
          description: 'Writer File (*.wtr)',
          extensions: ['wtr']
        }],
        acceptsAllTypes: true
      }, exportToFileEntry);
    } else if (fileName == 'html') {
      chrome.fileSystem.chooseEntry({
        type: 'saveFile',
        suggestedName: file,
        accepts: [{
          description: 'HTML Document (*.html)',
          extensions: ['html']
        }],
        acceptsAllTypes: true
      }, exportToFileEntry);
    } else if (fileName == 'md') {
      chrome.fileSystem.chooseEntry({
        type: 'saveFile',
        suggestedName: file,
        accepts: [{
          description: 'Markdown (*.md)',
          extensions: ['md']
        }],
        acceptsAllTypes: true
      }, exportToFileEntry);
    } else if (fileName == 'txt') {
      chrome.fileSystem.chooseEntry({
        type: 'saveFile',
        suggestedName: file,
        accepts: [{
          description: 'Text file (*.txt)',
          extensions: ['txt']
        }],
        acceptsAllTypes: true
      }, exportToFileEntry);
    } else {
      chrome.fileSystem.chooseEntry({
        type: 'saveFile',
        suggestedName: file,
        accepts: [{
          description: 'File ' + '(*' + '.' + fileName + ')',
          extensions: [fileName]
        }],
        acceptsAllTypes: true
      }, exportToFileEntry);
    }
  }
  $('.save-to-docx').click(function() {
    $('.ql-editor div').unhighlight();

    function exportToDocx(fileEntryDocx) {
      getTextAsHtml(function(contents) {
        fileEntryDocx.createWriter(function(fileWriter) {
          var truncated = false;
          var contents = '<!DOCTYPE HTML><head></head><body>' + editor.getHTML() + '</body></html>';
          var blob = htmlDocx.asBlob(contents);
          fileWriter.onwriteend = function(e) {
            if (!truncated) {
              truncated = true;
              // You need to explicitly set the file size to truncate
              // any content that might have been there before
              this.truncate(blob.size);
              return;
            }
          };
          fileWriter.write(blob);
        });
      });
    }
    var file = $('.doc-name').text();
    file = file.substring(0, file.lastIndexOf('.'));
    var fileName = file.split('.').pop();
    chrome.fileSystem.chooseEntry({
      type: 'saveFile',
      suggestedName: file + '.docx',
      accepts: [{
        description: 'Microsoft Word Document (*.docx)',
        extensions: ['docx']
      }],
      acceptsAllTypes: true
    }, exportToDocx);
  });
  $('#save').click(function() {
    if (savedFileEntry) {
      exportToFileEntry(savedFileEntry);
    } else {
      var file = $('.doc-name').text();
      var fileName = file.split('.').pop();
      if (fileName == 'wtr') {
        chrome.fileSystem.chooseEntry({
          type: 'saveFile',
          suggestedName: file,
          accepts: [{
            description: 'Writer File (*.wtr)',
            extensions: ['wtr']
          }],
          acceptsAllTypes: true
        }, exportToFileEntry);
      } else if (fileName == 'html') {
        chrome.fileSystem.chooseEntry({
          type: 'saveFile',
          suggestedName: file,
          accepts: [{
            description: 'HTML Document (*.html)',
            extensions: ['html']
          }],
          acceptsAllTypes: true
        }, exportToFileEntry);
      } else if (fileName == 'md') {
        chrome.fileSystem.chooseEntry({
          type: 'saveFile',
          suggestedName: file,
          accepts: [{
            description: 'Markdown (*.md)',
            extensions: ['md']
          }],
          acceptsAllTypes: true
        }, exportToFileEntry);
      } else if (fileName == 'txt') {
        chrome.fileSystem.chooseEntry({
          type: 'saveFile',
          suggestedName: file,
          accepts: [{
            description: 'Text file (*.txt)',
            extensions: ['txt']
          }],
          acceptsAllTypes: true
        }, exportToFileEntry);
      } else {
        chrome.fileSystem.chooseEntry({
          type: 'saveFile',
          suggestedName: file,
          accepts: [{
            description: 'File ' + '(*' + '.' + fileName + ')',
            extensions: [fileName]
          }],
          acceptsAllTypes: true
        }, exportToFileEntry);
      }
    }
  });
  $('.saveAs').click(function() {
    ExportToDisk();
  });
  //open document
  $('.open').click(function() {
    $('#modal2').openModal();
  });
  $('.open_real').click(function() {
    $('#modal2').closeModal();
    chrome.fileSystem.chooseEntry({
      type: 'openFile',
      accepts: [{
        mimeTypes: ['text/*'],
        extensions: ['wtr', 'txt', 'html', 'md']
      }]
    }, function(fileEntry) {
      savedFileEntry = fileEntry;
      documentEntry = chrome.fileSystem.retainEntry(fileEntry);
      chrome.storage.local.set({
        documententry: documentEntry
      });
      fileEntry.file(function(file) {
        var reader = new FileReader();
        var isSuccess = file.name.indexOf('.wtr') > -1;
        if (isSuccess) {
          clear();
          $('#txt').removeAttr('checked');
          $('#html').removeAttr('checked');
          $('#md').removeAttr('checked');
          $('#wtr').prop('checked', 'checked');
          $('#wtr').attr('checked', 'checked');
          $('.format-toggle .switch input').removeAttr('disabled');
          $('.format-toggle .switch input').attr('checked', 'checked');
          $('#full-toolbar').slideDown(200);
          $('#editor').css('height', 'calc(100% - 127px)');
          $('#editor').css('margin-top', '127px');
          $('#file_selector').attr('class', 'isWTR');
          $('#wtr').triggerHandler('click');
          saveOptions();
          reader.onloadend = function(e) {
            chrome.fileSystem.getDisplayPath(fileEntry, function(path) {
              var file = path.replace(/\\/g, '/').replace(/.*\//, '');
              var fileName = file.split('.').pop();
              $('.doc-name').text(file);
              if (file.indexOf('.') > -1) {
                $('.doc-type').text(fileName);
              } else {
                $('.doc-type').text('File');
              }
              $('.brand-logo').text('Writer - ' + file);
              saveFileName();
            });
            editor.setHTML(e.target.result);
            wordCount();
            save();
          };
          reader.readAsText(file);
          saved = 1;
        } else if (!isSuccess && file.name.indexOf('.html') > -1) {
          clear();
          $('#txt').removeAttr('checked');
          $('#md').removeAttr('checked');
          $('#wtr').removeAttr('checked');
          $('#html').prop('checked', 'checked');
          $('#html').attr('checked', 'checked');
          $('.format-toggle .switch input').removeAttr('disabled');
          $('.format-toggle .switch input').attr('checked', 'checked');
          $('#full-toolbar').slideDown(200);
          $('#editor').css('height', 'calc(100% - 127px)');
          $('#editor').css('margin-top', '127px');
          $('#file_selector').attr('class', 'isHTML');
          $('#html').triggerHandler('click');
          saveOptions();
          reader.onloadend = function(e) {
            chrome.fileSystem.getDisplayPath(fileEntry, function(path) {
              var file = path.replace(/\\/g, '/').replace(/.*\//, '');
              var fileName = file.split('.').pop();
              $('.doc-name').text(file);
              if (file.indexOf('.') > -1) {
                $('.doc-type').text(fileName);
              } else {
                $('.doc-type').text('File');
              }
              $('.brand-logo').text('Writer - ' + file);
              saveFileName();
            });
            editor.setText(e.target.result);
            wordCount();
            save();
          };
          reader.readAsText(file);
          saved = 1;
        } else if (!isSuccess && file.name.indexOf('.md') > -1) {
          clear();
          $('#txt').removeAttr('checked');
          $('#wtr').removeAttr('checked');
          $('#html').removeAttr('checked');
          $('#md').prop('checked', 'checked');
          $('#md').attr('checked', 'checked');
          $('.format-toggle .switch input').removeAttr('disabled');
          $('.format-toggle .switch input').attr('checked', 'checked');
          $('#full-toolbar').slideDown(200);
          $('#editor').css('height', 'calc(100% - 127px)');
          $('#editor').css('margin-top', '127px');
          $('#file_selector').attr('class', 'isMD');
          $('#md').triggerHandler('click');
          saveOptions();
          reader.onloadend = function(e) {
            chrome.fileSystem.getDisplayPath(fileEntry, function(path) {
              var file = path.replace(/\\/g, '/').replace(/.*\//, '');
              var fileName = file.split('.').pop();
              $('.doc-name').text(file);
              if (file.indexOf('.') > -1) {
                $('.doc-type').text(fileName);
              } else {
                $('.doc-type').text('File');
              }
              $('.brand-logo').text('Writer - ' + file);
              saveFileName();
            });
            var result = e.target.result;
            var md = marked(result);
            editor.setHTML(md);
            wordCount();
            save();
          };
          reader.readAsText(file);
          saved = 1;
        } else if (!isSuccess && file.name.indexOf('.txt') > -1) {
          clear();
          $('#wtr').removeAttr('checked');
          $('#html').removeAttr('checked');
          $('#md').removeAttr('checked');
          $('#txt').prop('checked', 'checked');
          $('#txt').attr('checked', 'checked');
          $('.format-toggle .switch input').removeAttr('checked');
          $('.format-toggle .switch input').attr('disabled', 'disabled');
          $('#full-toolbar').slideUp(200);
          $('#editor').css('height', 'calc(100% - 92px)');
          $('#editor').css('margin-top', '92px');
          $('#file_selector').attr('class', 'isTXT');
          $('#txt').triggerHandler('click');
          saveOptions();
          reader.onloadend = function(e) {
            chrome.fileSystem.getDisplayPath(fileEntry, function(path) {
              var file = path.replace(/\\/g, '/').replace(/.*\//, '');
              var fileName = file.split('.').pop();
              $('.doc-name').text(file);
              if (file.indexOf('.') > -1) {
                $('.doc-type').text(fileName);
              } else {
                $('.doc-type').text('File');
              }
              $('.brand-logo').text('Writer - ' + file);
              saveFileName();
            });
            editor.setText(e.target.result);
            wordCount();
            save();
          };
          reader.readAsText(file);
          saved = 1;
        } else if (!isSuccess) {
          clear();
          $('#wtr').removeAttr('checked');
          $('#html').removeAttr('checked');
          $('#md').removeAttr('checked');
          $('#txt').prop('checked', 'checked');
          $('#txt').attr('checked', 'checked');
          $('.format-toggle .switch input').removeAttr('checked');
          $('.format-toggle .switch input').attr('disabled', 'disabled');
          $('#full-toolbar').slideUp(200);
          $('#editor').css('height', 'calc(100% - 92px)');
          $('#editor').css('margin-top', '92px');
          $('#file_selector').attr('class', 'isTXT');
          $('#txt').triggerHandler('click');
          saveOptions();
          reader.onloadend = function(e) {
            chrome.fileSystem.getDisplayPath(fileEntry, function(path) {
              var file = path.replace(/\\/g, '/').replace(/.*\//, '');
              var fileName = file.split('.').pop();
              $('.doc-name').text(file);
              if (file.indexOf('.') > -1) {
                $('.doc-type').text(fileName);
              } else {
                $('.doc-type').text('File');
              }
              $('.brand-logo').text('Writer - ' + file);
              saveFileName();
            });
            editor.setText(e.target.result);
            wordCount();
            save();
          };
          reader.readAsText(file);
          saved = 1;
        }
        saveOptions();
      });
    });
  });
  //print document
  $('.print').click(function() {
    $('#file').closeModal();
    setTimeout(function() {
      $('.textarea_copy_print').show();
      var textVal = editor.getHTML();
      var textFont = $('.ql-editor').css('font-family');
      var textSize = $('.ql-editor').css('font-size');
      var lineHeight = $('.ql-editor').css('line-height');
      $('.textarea_copy_print').css('font-family', textFont);
      $('.textarea_copy_print').css('font-size', textSize);
      $('.textarea_copy_print').css('line-height', lineHeight);
      $('.textarea_copy_print').html(textVal);
      window.print();
    }, 300);
  });
  //templates
  $('.letter').click(function() {
    clear();
    $('#wtr').click();
    editor.setHTML('<div><b><span style="font-family: Roboto;"><span style="font-size: 14px;">Matthew Stacey</span></span></b></div><div><span style="color: rgb(102, 102, 102); font-family: Roboto; font-size: 14px; background-color: transparent;">123 Address St </span></div><div><span style="color: rgb(102, 102, 102); font-family: Roboto; font-size: 14px; background-color: transparent;">Anytown, NY 10011</span></div><div><span style="color: rgb(102, 102, 102); font-family: Roboto; font-size: 14px; background-color: transparent;">(123) 456-7890</span></div><div><span style="color: rgb(102, 102, 102); font-family: Roboto; font-size: 14px; background-color: transparent;">no_reply@example.com</span></div><div><br></div><div><span style="color: rgb(53, 55, 68); font-family: Roboto; font-size: 14px; background-color: transparent;">4th September 20XX</span></div><div><br></div><div><span style="color: rgb(53, 55, 68); font-family: Roboto; font-size: 14px; background-color: transparent;"><b>Jennifer Hernandez</b></span></div><div><span style="color: rgb(53, 55, 68); font-family: Roboto; font-size: 14px; background-color: transparent;">CEO, Parker Tech</span></div><div><span style="color: rgb(53, 55, 68); font-family: Roboto; font-size: 14px; background-color: transparent;">74 Lincoln Green Lane</span></div><div><span style="color: rgb(53, 55, 68); font-family: Roboto; font-size: 14px; background-color: transparent;">Church Stoke, PA 19402</span></div><div><br></div><div><span style="color: rgb(53, 55, 68); font-family: Roboto; font-size: 14px; background-color: transparent;">Dear Ms. Hernandez,</span></div><div><br></div><div><span style="color: rgb(53, 55, 68); font-family: Roboto; font-size: 14px; background-color: transparent;">Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad minim veniam, quis nostrud exerci tation ullamcorper suscipit lobortis nisl ut aliquip ex ea commodo consequat.</span></div><div><br></div><div><span style="color: rgb(53, 55, 68); font-family: Roboto; font-size: 14px; background-color: transparent;">Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu feugiat nulla facilisis at vero eros et accumsan.</span></div><div><br></div><div><span style="color: rgb(53, 55, 68); font-family: Roboto; font-size: 14px; background-color: transparent;">Nam liber tempor cum soluta nobis eleifend option congue nihil imperdiet doming id quod mazim placerat facer possim assum. Typi non habent claritatem insitam; est usus legentis in iis qui facit eorum claritatem. Investigationes demonstraverunt lectores legere me lius quod ii legunt saepius. </span></div><div><br></div><div><span style="color: rgb(53, 55, 68); font-family: Roboto; font-size: 14px; background-color: transparent;">Sincerely,</span></div><div><br></div><div><br></div><div><span style="font-family: Roboto; font-size: 14px; background-color: transparent;"><b>Matthew Stacey</b></span></div>');
    $('.doc-name').text('letter.wtr');
    $('.brand-logo').text('Writer - letter.wtr');
    $('.doc-type').text('wtr');
    $('.one-half-line').click();
    fileSize();
    saveFileName();
    savedFileEntry = null;
    chrome.storage.local.set({
      documententry: savedFileEntry
    });
  });
  $('.memo').click(function() {
    clear();
    $('#wtr').click();
    editor.setHTML('<div style="text-align: center;"><span style="font-size: 18px;">Memo</span></div><div><br></div><div><b>To:</b>	Jeffrey Davison</div><div><br></div><div><b>From:</b>	Lord Johny Caprisun<b> </b></div><div><br></div><div><b>Date:</b>	January 15, 2015	</div><div><br></div><div><b>RE:</b>	Random Stuff</div><div><br></div><div>	</div><div style="text-align: justify;">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aliquam viverra enim leo, non imperdiet enim tincidunt in. Pellentesque non scelerisque est, eu pretium sapien. Integer arcu enim, pulvinar ac nisi vel, tristique placerat arcu. Vestibulum ultrices eros vel lacus tempor dictum. Nullam non quam sed leo posuere mollis. Aliquam vitae justo diam. Aliquam erat volutpat. Donec lacinia vulputate diam eu imperdiet. Aenean dignissim sit amet purus viverra sodales. Aliquam nec condimentum tortor, in tincidunt elit. Etiam quis varius sapien. Sed a hendrerit nisl, vel convallis mauris.</div><div style="text-align: justify;"><br></div><div style="text-align: justify;">Cras semper, lectus imperdiet hendrerit vehicula, sapien odio ultrices mauris, sit amet luctus leo dolor non purus. Morbi commodo dolor sed urna suscipit, vel posuere arcu iaculis. Fusce ac pretium nisl, non gravida orci. Sed bibendum justo eget arcu gravida, eget ullamcorper nulla bibendum. Proin eu odio a orci aliquam interdum et sed magna. Pellentesque elit risus, imperdiet ac consectetur ac, sollicitudin vulputate nisi. Nam interdum nunc nec hendrerit consequat.</div><div style="text-align: justify;"><br></div><div>Nunc rutrum velit sed ex cursus efficitur. Duis sodales pellentesque nunc. Duis varius congue dolor. Donec dictum dignissim quam in sollicitudin. Maecenas nec ligula lacinia, cursus turpis nec, semper tellus. Nullam in velit eget mauris sollicitudin viverra. Nam eu turpis non nunc tempus commodo ut at nisl. Nullam malesuada est eget egestas sodales. Donec sem ante, pellentesque in mi in, dictum rutrum elit. Pellentesque at ipsum in dui malesuada lacinia. Interdum et malesuada fames ac ante ipsum primis in faucibus. Sed eleifend congue ipsum. Etiam pharetra leo et ex molestie, at auctor neque feugiat. Mauris ipsum eros, sagittis in nunc id, blandit sodales ligula.</div>');
    $('.doc-name').text('memo.wtr');
    $('.brand-logo').text('Writer - memo.wtr');
    $('.doc-type').text('wtr');
    $('.one-half-line').click();
    fileSize();
    saveFileName();
    savedFileEntry = null;
    chrome.storage.local.set({
      documententry: savedFileEntry
    });
  });
  $('.essay').click(function() {
    clear();
    $('#wtr').click();
    editor.setHTML('<div><span style="font-family: Roboto;">First name, Last Name</span></div><div><span style="font-family: Roboto;">Teacher Name</span></div><div><span style="font-family: Roboto;">Course Name</span></div><div><span style="font-family: Roboto;">Date</span></div><div style="text-align: center;"><span style="font-family: Roboto;">Title</span></div><div style="text-align: justify;"><span style="font-family: Roboto;">	Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi convallis velit ex, at tempus mi tincidunt ac. Aenean iaculis fermentum laoreet. Vivamus tristique eros vitae est posuere scelerisque. Morbi eleifend ligula sed tristique imperdiet. Suspendisse lorem sapien, porttitor in metus et, lacinia rhoncus tellus. Donec dignissim risus ac malesuada lacinia. Donec sed sollicitudin tortor.</span></div><div style="text-align: justify;">	<span style="font-family: Roboto;">Nullam molestie eget ex in convallis. Phasellus tempus eros et velit elementum vestibulum. Proin tempus turpis aliquam ex vehicula, non fringilla leo porttitor. Nulla facilisi. In nec quam ornare dolor commodo ullamcorper vitae vitae risus. Fusce venenatis imperdiet lorem, vel dignissim nibh. Ut dignissim, leo sed eleifend tincidunt, purus risus scelerisque sapien, ut rutrum ante nunc non justo. Aenean ut molestie sem. In hac habitasse platea dictumst. Sed cursus ante laoreet velit iaculis interdum. Cras elementum volutpat eros et placerat. Aliquam erat volutpat. Praesent vel cursus mauris, id venenatis lectus.</span></div><div>	<span style="font-family: Roboto;">Nunc sit amet ipsum ante. Duis a mauris elementum, congue odio id, commodo nunc. Morbi nec ligula nisi. Fusce vitae auctor lorem. Phasellus maximus venenatis purus, id convallis tortor dapibus in. Vivamus ultrices venenatis nisi, in placerat lorem suscipit at. Donec blandit dignissim nunc, id convallis eros interdum vel.</span></div>');
    $('.doc-name').text('essay.wtr');
    $('.brand-logo').text('Writer - essay.wtr');
    $('.doc-type').text('wtr');
    $('.double-line').click();
    fileSize();
    saveFileName();
    savedFileEntry = null;
    chrome.storage.local.set({
      documententry: savedFileEntry
    });
  });
  //text-editor functions
  //undo
  $('#undo').click(function() {
    var undoManager = editor.getModule('undo-manager');
    undoManager.undo();
  });
  //redo
  $('#redo').click(function() {
    var undoManager = editor.getModule('undo-manager');
    undoManager.redo();
  });
  //cut
  $('#cut').click(function() {
    document.execCommand('cut');
  });
  //copy
  $('#copy').click(function() {
    document.execCommand('copy');
  });
  //key combinations
  $(document).on('keydown', function(e) {
    if(e.keyCode == 122){
      $('.full-toggle-input').click();
    }
    if (e.ctrlKey && e.keyCode == 89) {
      var undoManager = editor.getModule('undo-manager'); // Assuming quill is your editor
      undoManager.undo();
    }
    //coffee
    if (e.ctrlKey && e.keyCode == 69) {
      e.preventDefault();
      $('.coffee-toggle-input').click();
    }
    //search
    if (e.ctrlKey && e.keyCode == 70) {
      $('.search-button').click();
    }
    //new
    if (e.ctrlKey && e.keyCode == 78) {
      e.preventDefault();
      $('#modal2').closeModal();
      $('#modal4').closeModal();
      $('#modal3').closeModal();
      $('#file').closeModal();
      $('#edit').closeModal();
      $('#help').closeModal();
      $('#word-count').closeModal();
      $('#details').closeModal();
      $('#osl').closeModal();
      if ($('#modal1').is(':visible')) {
        $('#modal1').closeModal();
      } else {
        $('#modal1').openModal();
      }
    }
    //save
    if (e.ctrlKey && e.keyCode == 83) {
      var file = $('.doc-name').text();
      var fileName = file.split('.').pop();
      e.preventDefault();
      if (savedFileEntry) {
        exportToFileEntry(savedFileEntry);
      } else {
        if (fileName == 'wtr') {
          chrome.fileSystem.chooseEntry({
            type: 'saveFile',
            suggestedName: file,
            accepts: [{
              description: 'Writer File (*.wtr)',
              extensions: ['wtr']
            }],
            acceptsAllTypes: true
          }, exportToFileEntry);
        } else if (fileName == 'html') {
          chrome.fileSystem.chooseEntry({
            type: 'saveFile',
            suggestedName: file,
            accepts: [{
              description: 'HTML Document (*.html)',
              extensions: ['html']
            }],
            acceptsAllTypes: true
          }, exportToFileEntry);
        } else if (fileName == 'md') {
          chrome.fileSystem.chooseEntry({
            type: 'saveFile',
            suggestedName: file,
            accepts: [{
              description: 'Markdown (*.md)',
              extensions: ['md']
            }],
            acceptsAllTypes: true
          }, exportToFileEntry);
        } else if (fileName == 'txt') {
          chrome.fileSystem.chooseEntry({
            type: 'saveFile',
            suggestedName: file,
            accepts: [{
              description: 'Text file (*.txt)',
              extensions: ['txt']
            }],
            acceptsAllTypes: true
          }, exportToFileEntry);
        } else {
          chrome.fileSystem.chooseEntry({
            type: 'saveFile',
            suggestedName: file,
            accepts: [{
              description: 'File ' + '(*' + '.' + fileName + ')',
              extensions: [fileName]
            }],
            acceptsAllTypes: true
          }, exportToFileEntry);
        }
      }
    }
    //saveas
    if (!e.metaKey) {
      if (e.shiftKey && e.ctrlKey && e.keyCode == 83) {
        e.preventDefault();
        ExportToDisk();
      }
    }
    //open
    if (e.ctrlKey && e.keyCode == 79) {
      e.preventDefault();
      $('#modal1').closeModal();
      $('#modal4').closeModal();
      $('#modal3').closeModal();
      $('#file').closeModal();
      $('#edit').closeModal();
      $('#help').closeModal();
      $('#word-count').closeModal();
      $('#details').closeModal();
      $('#osl').closeModal();
      if ($('#modal2').is(':visible')) {
        $('#modal2').closeModal();
      } else {
        $('#modal2').openModal();
      }
    }
    //print
    if (e.ctrlKey && e.keyCode == 80) {
      $('.print').click();
    }
    //nightmode
    if (e.ctrlKey && e.keyCode == 77) {
      $('.night-toggle-input').click();
    }
    //page
    if (e.ctrlKey && e.keyCode == 84) {
      e.preventDefault();
      $('.card-toggle-input').click();
    }
    //statistics
    if (e.ctrlKey && e.keyCode == 71) {
      e.preventDefault();
      $('.statistics-toggle-input').click();
    }
    //format
    if (e.ctrlKey && e.keyCode == 74) {
      e.preventDefault();
      $('.format-toggle-input').click();
    }
    //help
    if (e.ctrlKey && e.keyCode == 72) {
      $('#modal1').closeModal();
      $('#modal2').closeModal();
      $('#modal4').closeModal();
      $('#file').closeModal();
      $('#edit').closeModal();
      $('#modal3').closeModal();
      $('#word-count').closeModal();
      $('#details').closeModal();
      $('#osl').closeModal();
      if ($('#help').is(':visible')) {
        $('#help').closeModal();
      } else {
        $('.help').click();
      }
    }
    //word-count
    if (e.ctrlKey && e.keyCode == 87) {
      e.preventDefault();
      $('#modal1').closeModal();
      $('#modal2').closeModal();
      $('#modal4').closeModal();
      $('#file').closeModal();
      $('#edit').closeModal();
      $('#modal3').closeModal();
      $('#help').closeModal();
      $('#details').closeModal();
      $('#osl').closeModal();
      if ($('#word-count').is(':visible')) {
        $('#word-count').closeModal();
      } else {
        $('.word-count').click();
      }
    }
    //details
    if (e.ctrlKey && e.keyCode == 68) {
      $('#modal1').closeModal();
      $('#modal2').closeModal();
      $('#file').closeModal();
      $('#edit').closeModal();
      $('#modal4').closeModal();
      $('#modal3').closeModal();
      $('#help').closeModal();
      $('#word-count').closeModal();
      $('#osl').closeModal();
      if ($('#details').is(':visible')) {
        $('#details').closeModal();
      } else {
        $('.details').click();
      }
    }
  });
  //window bar functions
  $("#close").click(function() {
    $('.ql-editor div').unhighlight();
    window.close();
  });
  $("#minimize").click(function() {
    chrome.app.window.current().minimize();
  });
  $("#maximize").click(function() {
    if (chrome.app.window.current().isMaximized() === true) {
      chrome.app.window.current().restore();
    } else {
      chrome.app.window.current().maximize();
    }
    if (chrome.app.window.current().isFullscreen() === true) {
      chrome.app.window.current().restore();
      $('.full-toggle-input').removeAttr('checked');
    }
    editor.focus();
  });
  //save and load options
  function saveOptions() {
    var format = $('#full-toolbar').css('display');
    var statistics = $('.statistics').css('display');
    var cardNight = $('#selector').attr('class');
    var filetype = $('#file_selector').attr('class');
    var theme = $('.nav-wrapper').css('background-color');
    var accent = $('.btn-floating').attr('class');
    var welcomeCheck = $('#welcome').attr('class');
    var font = $('#editor').css('font-family');
    var fontsize = $('#editor').css('font-size');
    var marginSize = margin;
    var volume = vol;
    var actualAccentColor = $('.btn-floating').css('background-color');
    //document details
    var docName = $('.doc-name').text();
    var docType = $('.doc-type').text();
    var docSize = $('.doc-size').text();
    chrome.storage.local.set({
      formatToggle: format,
      statisticsToggle: statistics,
      cardToggle: cardNight,
      fileType: filetype,
      themeColor: theme,
      accentColor: accent,
      welcomecheck: welcomeCheck,
      defaultFont: font,
      docname: docName,
      doctype: docType,
      docsize: docSize,
      marginsize: margin,
      Volume: volume,
      defaultFontSize: fontsize,
      actualaccentcolor: actualAccentColor
    });
  }

  function loadOptions() {
    chrome.storage.local.get({
      formatToggle: 'format',
      statisticsToggle: 'statistics',
      cardToggle: 'cardNight',
      fileType: 'filetype',
      themeColor: 'theme',
      accentColor: 'accent',
      welcomecheck: 'welcomeCheck',
      defaultFont: 'font',
      docname: 'docName',
      doctype: 'docType',
      docsize: 'docSize',
      marginsize: 'margin',
      Volume: 'volume',
      defaultFontSize: 'fontsize',
      actualaccentcolor: 'actualAccentColor'
    }, function(options) {
      var formatToggle = options.formatToggle;
      var statisticsToggle = options.statisticsToggle;
      var cardToggle = options.cardToggle;
      var fileType = options.fileType;
      var themeColor = options.themeColor;
      var accentColor = options.accentColor;
      var welcomecheck = options.welcomecheck;
      var defaultFont = options.defaultFont;
      var marginsize = options.marginsize;
      var defaultFontSize = options.defaultFontSize;
      var actualaccentcolor = options.actualaccentcolor;
      var Volume = options.Volume;

      //coffee mode
      if (Volume < 0.5 || Volume == 'volume') {
        audioElement.volume = 0;
        $('.coffee-toggle-input').removeAttr('checked');
      } else {
        audioElement.volume = Volume;
        playSound('assets/sounds/CoffeeShop.mp4');
        $('.coffee-toggle-input').attr('checked', 'checked');
      }

      //default font size
      if (defaultFontSize == 'fontsize') {
        $('.ql-size option').removeAttr('selected');
        $('.sixteen-option').attr('selected', '');
        $('.ql-size .ql-picker-options .ql-picker-item').removeClass('ql-selected');
        $('.ql-size .ql-picker-options .ql-picker-item[data-value="16px"]').addClass('ql-selected').click();
        $('#editor').css('font-size', '16px');
        $('#default_font_size .modal-content p input').removeAttr('checked');
        $('#fontsize-sixteen').prop('checked', true);
      }
      if (defaultFontSize == '10px') {
        $('.ql-size option').removeAttr('selected');
        $('.ten-option').attr('selected', '');
        $('.ql-size .ql-picker-options .ql-picker-item').removeClass('ql-selected');
        $('.ql-size .ql-picker-options .ql-picker-item[data-value="10px"]').addClass('ql-selected').click();
        $('#editor').css('font-size', '10px');
        $('#default_font_size .modal-content p input').removeAttr('checked');
        $('#fontsize-ten').prop('checked', true);
      }
      if (defaultFontSize == '12px') {
        $('.ql-size option').removeAttr('selected');
        $('.twelve-option').attr('selected', '');
        $('.ql-size .ql-picker-options .ql-picker-item').removeClass('ql-selected');
        $('.ql-size .ql-picker-options .ql-picker-item[data-value="12px"]').addClass('ql-selected').click();
        $('#editor').css('font-size', '12px');
        $('#default_font_size .modal-content p input').removeAttr('checked');
        $('#fontsize-twelve').prop('checked', true);
      }
      if (defaultFontSize == '16px') {
        $('.ql-size option').removeAttr('selected');
        $('.sixteen-option').attr('selected', '');
        $('.ql-size .ql-picker-options .ql-picker-item').removeClass('ql-selected');
        $('.ql-size .ql-picker-options .ql-picker-item[data-value="16px"]').addClass('ql-selected').click();
        $('#editor').css('font-size', '16px');
        $('#default_font_size .modal-content p input').removeAttr('checked');
        $('#fontsize-sixteen').prop('checked', true);
      }
      if (defaultFontSize == '18px') {
        $('.ql-size option').removeAttr('selected');
        $('.eighteen-option').attr('selected', '');
        $('.ql-size .ql-picker-options .ql-picker-item').removeClass('ql-selected');
        $('.ql-size .ql-picker-options .ql-picker-item[data-value="18px"]').addClass('ql-selected').click();
        $('#editor').css('font-size', '18px');
        $('#default_font_size .modal-content p input').removeAttr('checked');
        $('#fontsize-eighteen').prop('checked', true);
      }
      if (defaultFontSize == '24px') {
        $('.ql-size option').removeAttr('selected');
        $('.twentyfour-option').attr('selected', '');
        $('.ql-size .ql-picker-options .ql-picker-item').removeClass('ql-selected');
        $('.ql-size .ql-picker-options .ql-picker-item[data-value="24px"]').addClass('ql-selected').click();
        $('#editor').css('font-size', '24px');
        $('#default_font_size .modal-content p input').removeAttr('checked');
        $('#fontsize-twentyfour').prop('checked', true);
      }
      if (defaultFontSize == '32px') {
        $('.ql-size option').removeAttr('selected');
        $('.thirtytwo-option').attr('selected', '');
        $('.ql-size .ql-picker-options .ql-picker-item').removeClass('ql-selected');
        $('.ql-size .ql-picker-options .ql-picker-item[data-value="32px"]').addClass('ql-selected').click();
        $('#editor').css('font-size', '32px');
        $('#default_font_size .modal-content p input').removeAttr('checked');
        $('#fontsize-thirtytwo').prop('checked', true);
      }
      if (defaultFontSize == '48px') {
        $('.ql-size option').removeAttr('selected');
        $('.fourtyeight-option').attr('selected', '');
        $('.ql-size .ql-picker-options .ql-picker-item').removeClass('ql-selected');
        $('.ql-size .ql-picker-options .ql-picker-item[data-value="48px"]').addClass('ql-selected').click();
        $('#editor').css('font-size', '48px');
        $('#default_font_size .modal-content p input').removeAttr('checked');
        $('#fontsize-fourtyeight').prop('checked', true);
      }
      //document details
      var docname = options.docname;
      var doctype = options.doctype;
      var docsize = options.docsize;
      if (docname == 'docName' || doctype == 'docType' || docsize == 'docsize') {} else {
        $('.doc-name').text(docname);
        $('.doc-type').text(doctype);
        $('.doc-size').text(docsize);
      }
      //margin-size
      if (marginsize == 'margin') {
        $('#margin_size .modal-content p input').removeAttr('checked');
        $('#five').prop('checked', true);
        $('#editor').css('padding-left', '5%');
        $('#editor').css('padding-right', '5%');
      } else if (marginsize === 0) {
        $('#margin_size .modal-content p input').removeAttr('checked');
        $('#fifteen').prop('checked', true);
        $('#editor').css('padding-left', '15%');
        $('#editor').css('padding-right', '15%');
        margin = 0;
        $('#zero').click();
      } else if (marginsize == 5) {
        $('#margin_size .modal-content p input').removeAttr('checked');
        $('#five').prop('checked', true);
        $('#editor').css('padding-left', '5%');
        $('#editor').css('padding-right', '5%');
        margin = 5;
      } else if (marginsize == 10) {
        $('#margin_size .modal-content p input').removeAttr('checked');
        $('#ten').prop('checked', true);
        $('#editor').css('padding-left', '10%');
        $('#editor').css('padding-right', '10%');
        margin = 10;
      } else if (marginsize == 15) {
        $('#margin_size .modal-content p input').removeAttr('checked');
        $('#fifteen').prop('checked', true);
        $('#editor').css('padding-left', '15%');
        $('#editor').css('padding-right', '15%');
        margin = 15;
      } else if (marginsize == 20) {
        $('#margin_size .modal-content p input').removeAttr('checked');
        $('#twenty').prop('checked', true);
        $('#editor').css('padding-left', '20%');
        $('#editor').css('padding-right', '20%');
        margin = 20;
      } else if (marginsize == 25) {
        $('#margin_size .modal-content p input').removeAttr('checked');
        $('#twenty-five').prop('checked', true);
        $('#editor').css('padding-left', '25%');
        $('#editor').css('padding-right', '25%');
        margin = 25;
      }
      //format
      if (formatToggle == 'format') {
        $('#full-toolbar').slideDown(200);
        $('#editor').css('height', 'calc(100% - 127px)');
        $('#editor').css('margin-top', '127px');
        $('.format-toggle-input').attr('checked', 'checked');
      } else if (formatToggle == 'none') {
        $('#full-toolbar').slideUp(200);
        $('#editor').css('height', 'calc(100% - 92px)');
        $('#editor').css('margin-top', '92px');
        $('.format-toggle-input').removeAttr('checked');
      } else if (formatToggle == 'block') {
        $('#full-toolbar').slideDown(200);
        $('#editor').css('height', 'calc(100% - 127px)');
        $('#editor').css('margin-top', '127px');
        $('.format-toggle-input').attr('checked', 'checked');
      }
      //statistics
      if (statisticsToggle == 'statistics') {
        $('.statistics').slideUp(200);
        $('.statistics-toggle-input').removeAttr('checked');
      } else if (statisticsToggle == 'none') {
        $('.statistics').slideUp(200);
        $('.statistics-toggle-input').removeAttr('checked');
      } else if (statisticsToggle == 'block') {
        $('.statistics').slideDown(200);
        $('.statistics-toggle-input').attr('checked', 'checked');
      }
      //card and nightmode
      if (cardToggle == 'cardNight') {
        $('.ql-editor').removeClass('card');
        $('.ql-editor').removeClass('card-nightmode');
        $('#editor').css('background-color', '#FFF');
        $('.night-toggle-input').removeAttr('checked');
        $('.card-toggle-input').removeAttr('checked');
        $('#editor').css('color', '#000');
        $('#selector').removeAttr('class');
        $('#selector').addClass('DoesNotHaveCardDoesNotHaveNight');
        $('#full-toolbar').attr('class', 'toolbar ql-toolbar ql-snow grey lighten-4');
        $('.statistics').attr('class', 'statistics z-depth-1 grey lighten-4');
        $('#words').attr('class', 'words grey-text text-darken-4');
        $('#characters').attr('class', 'characters grey-text text-darken-4');
      }
      if (cardToggle == 'HasCardDoesNotHaveNight') {
        $('.ql-editor').addClass('card');
        $('.ql-editor').removeClass('card-nightmode');
        $('#editor').css('background-color', '#EEE');
        $('.card-toggle-input').attr('checked', 'checked');
        $('.night-toggle-input').removeAttr('checked');
        $('#editor').css('color', '#000');
        $('#selector').removeAttr('class');
        $('#selector').addClass('HasCardDoesNotHaveNight');
        $('#full-toolbar').attr('class', 'toolbar ql-toolbar ql-snow grey lighten-4');
        $('.statistics').attr('class', 'statistics z-depth-1 grey lighten-4');
        $('#words').attr('class', 'words grey-text text-darken-4');
        $('#characters').attr('class', 'characters grey-text text-darken-4');
      }
      if (cardToggle == 'HasCardHasNight') {
        $('.ql-editor').removeClass('card');
        $('.ql-editor').addClass('card-nightmode');
        $('#editor').css('background-color', '#101010');
        $('.card-toggle-input').attr('checked', 'checked');
        $('.night-toggle-input').attr('checked', 'checked');
        $('#editor').css('color', '#FFF');
        $('#selector').removeAttr('class');
        $('#selector').addClass('HasCardHasNight');
        $('#full-toolbar').attr('class', 'toolbar ql-toolbar ql-snow grey darken-4');
        $('.statistics').attr('class', 'statistics z-depth-1 grey darken-4');
        $('#words').attr('class', 'words grey-text text-lighten-4');
        $('#characters').attr('class', 'characters grey-text text-lighten-4');
      }
      if (cardToggle == 'DoesNotHaveCardDoesNotHaveNight') {
        $('.ql-editor').removeClass('card');
        $('.ql-editor').removeClass('card-nightmode');
        $('#editor').css('background-color', '#FFF');
        $('.night-toggle-input').removeAttr('checked');
        $('.card-toggle-input').removeAttr('checked');
        $('#editor').css('color', '#000');
        $('#selector').removeAttr('class');
        $('#selector').addClass('DoesNotHaveCardDoesNotHaveNight');
        $('#full-toolbar').attr('class', 'toolbar ql-toolbar ql-snow grey lighten-4');
        $('.statistics').attr('class', 'statistics z-depth-1 grey lighten-4');
        $('#words').attr('class', 'words grey-text text-darken-4');
        $('#characters').attr('class', 'characters grey-text text-darken-4');
      }
      if (cardToggle == 'DoesNotHaveCardHasNight') {
        $('.ql-editor').removeClass('card');
        $('.ql-editor').removeClass('card-nightmode');
        $('#editor').css('background-color', '#101010');
        $('.card-toggle-input').removeAttr('checked');
        $('.night-toggle-input').attr('checked', 'checked');
        $('#editor').css('color', '#FFF');
        $('#selector').removeAttr('class');
        $('#selector').addClass('DoesNotHaveCardHasNight');
        $('#full-toolbar').attr('class', 'toolbar ql-toolbar ql-snow grey darken-4');
        $('.statistics').attr('class', 'statistics z-depth-1 grey darken-4');
        $('#words').attr('class', 'words grey-text text-lighten-4');
        $('#characters').attr('class', 'characters grey-text text-lighten-4');
      }
      //welcome menu
      if (welcomecheck == 'welcomeCheck') {
        $('.welcome-toggle-input').attr('checked', 'checked');
        $('#welcome').openModal();
      }
      if (welcomecheck == 'modal welcome bottom-sheet hide') {
        $('#welcome').closeModal();
        $('#welcome').attr('class', welcomecheck);
        $('.welcome-toggle-input').removeAttr('checked');
        editor.focus();
      }
      if (welcomecheck == 'modal welcome bottom-sheet show') {
        $('#welcome').openModal();
        $('#welcome').attr('class', welcomecheck);
        $('.welcome-toggle-input').attr('checked', 'checked');
        editor.focus();
      }
      //themes and accents
      $('.nav-wrapper').css('background-color', themeColor);
      if ($.inArray(themeColor, ['rgb(3, 169, 244)', 'rgb(0, 188, 212)', 'rgb(76, 175, 80)', 'rgb(139, 195, 74)', 'rgb(205, 220, 57)', 'rgb(255, 235, 59)', 'rgb(255, 193, 7)', 'rgb(255, 152, 0)', 'rgb(158, 158, 158)']) >= 0) {
        $('nav .button-collapse i, .brand-logo, .nav-wrapper .material-icons, .search-field').css('color', 'rgba(0,0,0,0.87)');
        $('.line').css('background-color', 'rgba(0,0,0,0.87)');
      } else {
        $('nav .button-collapse i, .brand-logo, .nav-wrapper .material-icons, .search-field').css('color', 'rgba(255,255,255,0.87)');
        $('.line').css('background-color', 'rgba(255,255,255,0.87)');
      }
      if (accentColor == 'accent') {} else {
        $('.btn-floating').attr('class', accentColor);
      }
      if ($.inArray(actualaccentcolor, ['rgb(3, 169, 244)', 'rgb(0, 188, 212)', 'rgb(76, 175, 80)', 'rgb(139, 195, 74)', 'rgb(205, 220, 57)', 'rgb(255, 235, 59)', 'rgb(255, 193, 7)', 'rgb(255, 152, 0)', 'rgb(158, 158, 158)']) >= 0) {
        $('.btn-floating i').css('color', 'rgba(0,0,0,0.87)');
      } else {
        $('.btn-floating i').css('color', 'rgba(255,255,255,0.87)');
      }
      //default font
      if (defaultFont == 'Roboto' || defaultFont == 'font') {
        $('.ql-font option').removeAttr('selected');
        $('.roboto-option').attr('selected', '');
        $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
        $('.ql-font .ql-picker-options .ql-picker-item[data-value="Roboto"]').addClass('ql-selected').click();
        $('#editor').css('font-family', 'Roboto');
        $('#default_font .modal-content p input').removeAttr('checked');
        $('#roboto-font').prop('checked', true);
      }
      if (defaultFont == 'Georgia') {
        $('.ql-font option').removeAttr('selected');
        $('.georgia-option').attr('selected', '');
        $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
        $('.ql-font .ql-picker-options .ql-picker-item[data-value="Georgia"]').addClass('ql-selected').click();
        $('#editor').css('font-family', 'Georgia');
        $('#default_font .modal-content p input').removeAttr('checked');
        $('#georgia-font').prop('checked', true);
      }
      if (defaultFont == 'Arial') {
        $('.ql-font option').removeAttr('selected');
        $('.arial-option').attr('selected', '');
        $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
        $('.ql-font .ql-picker-options .ql-picker-item[data-value="Arial"]').addClass('ql-selected').click();
        $('#editor').css('font-family', 'Arial');
        $('#default_font .modal-content p input').removeAttr('checked');
        $('#arial-font').prop('checked', true);
      }
      if (defaultFont == 'Calibri') {
        $('.ql-font option').removeAttr('selected');
        $('.calibri-option').attr('selected', '');
        $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
        $('.ql-font .ql-picker-options .ql-picker-item[data-value="Calibri"]').addClass('ql-selected').click();
        $('#editor').css('font-family', 'Calibri');
        $('#default_font .modal-content p input').removeAttr('checked');
        $('#calibri-font').prop('checked', true);
      }
      if (defaultFont == "'Comic Sans MS'") {
        $('.ql-font option').removeAttr('selected');
        $('.comic-sans-option').attr('selected', '');
        $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
        $('.ql-font .ql-picker-options .ql-picker-item[data-value="Comic Sans MS"]').addClass('ql-selected').click();
        $('#editor').css('font-family', 'Comic Sans MS');
        $('#default_font .modal-content p input').removeAttr('checked');
        $('#comic-sans-font').prop('checked', true);
      }
      if (defaultFont == "Courier") {
        $('.ql-font option').removeAttr('selected');
        $('.courier-option').attr('selected', '');
        $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
        $('.ql-font .ql-picker-options .ql-picker-item[data-value="Courier"]').addClass('ql-selected').click();
        $('#editor').css('font-family', 'Courier');
        $('#default_font .modal-content p input').removeAttr('checked');
        $('#courier-font').prop('checked', true);
      }
      if (defaultFont == 'Helvetica') {
        $('.ql-font option').removeAttr('selected');
        $('.helvetica-option').attr('selected', '');
        $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
        $('.ql-font .ql-picker-options .ql-picker-item[data-value="Helvetica"]').addClass('ql-selected').click();
        $('#editor').css('font-family', 'Helvetica');
        $('#default_font .modal-content p input').removeAttr('checked');
        $('#helvetica-font').prop('checked', true);
      }
      if (defaultFont == "'Open Sans'") {
        $('.ql-font option').removeAttr('selected');
        $('.open-sans-option').attr('selected', '');
        $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
        $('.ql-font .ql-picker-options .ql-picker-item[data-value="Open Sans"]').addClass('ql-selected').click();
        $('#editor').css('font-family', 'Open Sans');
        $('#default_font .modal-content p input').removeAttr('checked');
        $('#open-sans-font').prop('checked', true);
      }
      if (defaultFont == "Palatino") {
        $('.ql-font option').removeAttr('selected');
        $('.palatino-option').attr('selected', '');
        $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
        $('.ql-font .ql-picker-options .ql-picker-item[data-value="Palatino"]').addClass('ql-selected').click();
        $('#editor').css('font-family', 'Palatino');
        $('#default_font .modal-content p input').removeAttr('checked');
        $('#palatino-font').prop('checked', true);
      }
      if (defaultFont == "'Trebuchet MS'") {
        $('.ql-font option').removeAttr('selected');
        $('.trebuchet-option').attr('selected', '');
        $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
        $('.ql-font .ql-picker-options .ql-picker-item[data-value="Trebuchet MS"]').addClass('ql-selected').click();
        $('#editor').css('font-family', 'Trebuchet MS');
        $('#default_font .modal-content p input').removeAttr('checked');
        $('#trebuchet-font').prop('checked', true);
      }
      if (defaultFont == 'sans-serif') {
        $('.ql-font option').removeAttr('selected');
        $('.sans-serif-option').attr('selected', '');
        $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
        $('.ql-font .ql-picker-options .ql-picker-item[data-value="sans-serif"]').addClass('ql-selected').click();
        $('#editor').css('font-family', 'sans-serif');
        $('#default_font .modal-content p input').removeAttr('checked');
        $('#sans-serif-font').prop('checked', true);
      }
      if (defaultFont == 'serif') {
        $('.ql-font option').removeAttr('selected');
        $('.serif-option').attr('selected', '');
        $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
        $('.ql-font .ql-picker-options .ql-picker-item[data-value="serif"]').addClass('ql-selected').click();
        $('#editor').css('font-family', 'serif');
        $('#default_font .modal-content p input').removeAttr('checked');
        $('#serif-font').prop('checked', true);
      }
      if (defaultFont == 'monospace') {
        $('.ql-font option').removeAttr('selected');
        $('.monospace-option').attr('selected', '');
        $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
        $('.ql-font .ql-picker-options .ql-picker-item[data-value="monospace"]').addClass('ql-selected').click();
        $('#editor').css('font-family', 'monospace');
        $('#default_font .modal-content p input').removeAttr('checked');
        $('#monospace-font').prop('checked', true);
      }
      if (defaultFont == "'Times New Roman'") {
        $('.ql-font option').removeAttr('selected');
        $('.times-option').attr('selected', '');
        $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
        $('.ql-font .ql-picker-options .ql-picker-item[data-value="Times New Roman"]').addClass('ql-selected').click();
        $('#editor').css('font-family', 'Times New Roman');
        $('#default_font .modal-content p input').removeAttr('checked');
        $('#times-font').prop('checked', true);
      }
      if (defaultFont == 'Verdana') {
        $('.ql-font option').removeAttr('selected');
        $('.verdana-option').attr('selected', '');
        $('.ql-font .ql-picker-options .ql-picker-item').removeClass('ql-selected');
        $('.ql-font .ql-picker-options .ql-picker-item[data-value="Verdana"]').addClass('ql-selected').click();
        $('#editor').css('font-family', 'Verdana');
        $('#default_font .modal-content p input').removeAttr('checked');
        $('#verdana-font').prop('checked', true);
      }
      //default file types
      if (fileType == 'filetype') {
        $('#wtr').prop('checked', true);
      }
      if (fileType == 'isWTR') {
        $('#txt').removeAttr('checked');
        $('#html').removeAttr('checked');
        $('#md').removeAttr('checked');
        $('#wtr').prop('checked', true);
        $('.format-toggle .switch input').removeAttr('disabled');
        $('.format-toggle .switch input').attr('checked', 'checked');
        $('#full-toolbar').slideDown(200);
        $('#editor').css('height', 'calc(100% - 127px)');
        $('#editor').css('margin-top', '127px');
        $('#file_selector').attr('class', 'isWTR');
      }
      if (fileType == 'isMD') {
        $('#txt').removeAttr('checked');
        $('#html').removeAttr('checked');
        $('#wtr').removeAttr('checked');
        $('#md').prop('checked', true);
        $('.format-toggle .switch input').removeAttr('disabled');
        $('.format-toggle .switch input').attr('checked', 'checked');
        $('#full-toolbar').slideDown(200);
        $('#editor').css('height', 'calc(100% - 127px)');
        $('#editor').css('margin-top', '127px');
        $('#file_selector').attr('class', 'isMD');
      }
      if (fileType == 'isHTML') {
        $('#txt').removeAttr('checked');
        $('#wtr').removeAttr('checked');
        $('#md').removeAttr('checked');
        $('#html').prop('checked', true);
        $('.format-toggle .switch input').removeAttr('disabled');
        $('.format-toggle .switch input').attr('checked', 'checked');
        $('#full-toolbar').slideDown(200);
        $('#editor').css('height', 'calc(100% - 127px)');
        $('#editor').css('margin-top', '127px');
        $('#file_selector').attr('class', 'isHTML');
      }
      if (fileType == 'isTXT') {
        $('#wtr').removeAttr('checked');
        $('#html').removeAttr('checked');
        $('#md').removeAttr('checked');
        $('#txt').prop('checked', true);
        $('.format-toggle .switch input').removeAttr('checked');
        $('.format-toggle .switch input').attr('disabled', 'disabled');
        $('#full-toolbar').slideUp(200);
        $('#editor').css('height', 'calc(100% - 92px)');
        $('#editor').css('margin-top', '92px');
        $('#file_selector').attr('class', 'isTXT');
      }
    });
  }
  $(window).bind('load', function() {
    //Google Analytics
    window.ga=window.ga||function(){(ga.q=ga.q||[]).push(arguments)};ga.l=+new Date;
    ga('create', 'UA-87756821-1', 'auto');
    ga('send', 'pageview');
    
    setTimeout(function() {
      $('.load').animate({
        top: '-100%',
        opacity: '0'
      }, 400, function() {
        $(this).hide();
      });
    }, 800);
    setTimeout(function() {
      load();
      fileSize();
      windowColor();
      lineSpacingLoad();
      spellLoad();
      wordCount();
    }, 200);
    loadOptions();
    if (chrome.app.window.current().isFullscreen() === true) {
      $('.full-toggle-input').prop('checked', true);
    } else {
      $('.full-toggle-input').removeAttr('checked');
    }
    //retrieve saved file entry
    //and pass it to savedFileEntry
    chrome.storage.local.get({
      documententry: 'documentEntry'
    }, function(documents) {
      var documententry = documents.documententry;
      if (documententry == 'documentEntry' || documententry === null) {
        savedFileEntry = null;
        loadFileName();
      } else {
        chrome.fileSystem.restoreEntry(documententry, function(entry) {
          savedFileEntry = entry;
          $('.doc-name').text(entry.name);
          $('.brand-logo').text('Writer - ' + entry.name);
        });
      }
    });
  });
});

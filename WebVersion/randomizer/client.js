var mapstoneCount = 0,
    trackerID,
    intervalHandle;

function updateMapstone() {
    // id is of format `mapstone-<inc|dec>`
    var id = this.id,
	words = id.split( '-' );

    if ( words[1] === 'inc' && mapstoneCount < 9 ) {
	mapstoneCount++;
    } else if ( words[1] === 'dec' && mapstoneCount > 0 ) {
	mapstoneCount--;
    }
    $( '#mapstoneText' ).text( mapstoneCount + '/9' );

    $.post( 'server.php', {
	match: trackerID,
	state: {
	    mapstones: mapstoneCount
	}
    }, function ( res ) {
    } );
}

function onCheckboxChange() {
    // id is of format `<ID of corresponding image>`
    var id = this.id,
	imgId = id.substr( 6 ),
	state = this.checked,
	data = {
	    match: trackerID,
	    state: {
		mapstones: mapstoneCount
	    }
	};

    data.state[imgId] = state;
    $( '#' + imgId ).toggleClass( 'show' );
    $( '#' + imgId + '-g' ).toggleClass( 'show' );

    $.post( 'server.php', data, function ( res ) {
	$( '#sync-message' )
	    .text( 'Data sent successfully.' )
	    .fadeIn( 50 )
	    .delay( 3000 )
	    .fadeOut( 1000 );
    } );
}

function updateState( state ) {
    mapstoneCount = state.mapstones;
    $( '#mapstoneText' ).text( mapstoneCount + '/9' );
    delete state.mapstones;

    var ids = Object.keys( state );

    // We want to find everything that's currently shown EXCEPT
    // those images which are shown *by default* since we're going
    // to check these results agains the provided state and hide
    // any that don't match the server.
    $( '.show' ).not( '[id$="-g"]' ).each( function ( _, $el ) {
	if ( ids.indexOf( $el.id ) < 0 ) {
	    $( this ).removeClass( 'show' );
	    $( '#' + $el.id + '-g' ).addClass( 'show' );
	    $( '#check-' + $el.id ).prop( 'checked', false );
	}
    } );

    // Light up everything sent by the server, even if we just turned it off
    ids.forEach( function ( id ) {
	$( '#' + id ).addClass( 'show' );
	$( '#' + id + '-g' ).removeClass( 'show' );
	$( '#check-' + id ).prop( 'checked', true );
    } );
}

function linkMatch( e ) {
    if ( intervalHandle ) {
	clearInterval( intervalHandle );
	intervalHandle = undefined;
	return false;
    }

    trackerID = getUrlParameter('tracker');
    
    $.get( 'server.php', { match: trackerID }, function ( res ) {
	updateState( res );
	intervalHandle = setInterval( function () {
	    $.get( 'server.php', { match: trackerID }, function ( res ) {
		updateState( res );
	    } );
	}, 250 );

	$( '#sync-message' )
	    .text( 'Link established for "' + trackerID + '" successfully. Now updating every 250ms.' )
	    .fadeIn( 50 )
	    .delay( 3000 )
	    .fadeOut( 1000 );
    } );

    e.preventDefault();
}

function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
	sURLVariables = sPageURL.split('&'),
	sParameterName,
	i;

    for (i = 0; i < sURLVariables.length; i++) {
	sParameterName = sURLVariables[i].split('=');

	if (sParameterName[0] === sParam) {
	    return sParameterName[1] === undefined ? true : sParameterName[1];
	}
    }
}

$( 'button' ).on( 'click', updateMapstone );
$( 'body' ).on( 'change', 'input[type="checkbox"]', onCheckboxChange );
$( linkMatch );

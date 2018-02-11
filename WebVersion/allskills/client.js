var matchId,
    intervalHandle;

function onRadioChange()
{
    var id1 = "radio-t1-place-first",
	imgId1 = id1.substr( 6 ),
	state1 = document.getElementById(id1).checked,
	id2 = "radio-t2-place-first",
	imgId2 = id2.substr( 6 ),
	state2 = document.getElementById(id2).checked,
	data = {
	    match: matchId,
	    state: {
	    }
	};

    if ( state1 )
    {
	$( '#' + imgId1 ).addClass( 'show' );
	$( '#' + imgId1 + '-g' ).removeClass( 'show' );
    }
    else
    {
	$( '#' + imgId1 ).removeClass( 'show' );
	$( '#' + imgId1 + '-g' ).addClass( 'show' );
    }

    if ( state2 )
    {
	$( '#' + imgId2 ).addClass( 'show' );
	$( '#' + imgId2 + '-g' ).removeClass( 'show' );
    }
    else
    {
	$( '#' + imgId2 ).removeClass( 'show' );
	$( '#' + imgId2 + '-g' ).addClass( 'show' );

    }

    data.state[imgId1] = state1;
    $.post( 'server.php', data, function ( res ) {
	$( '#sync-message' )
	    .text( 'First part sent successfully.' )
	    .fadeIn( 50 )
	    .delay( 3000 )
	    .fadeOut( 1000 );
    } );

    var data2 = {
	match: matchId,
	state: {
	}
    };
    
    data2.state[imgId2] = state2;
    $.post( 'server.php', data2, function ( res ) {
	$( '#sync-message' )
	    .text( 'Second part sent successfully.' )
	    .fadeIn( 50 )
	    .delay( 3000 )
	    .fadeOut( 1000 );
    } );
}

function onCheckboxChange() {
    // id is of format `<t1|t2>-<ID of corresponding image>`
    var id = this.id,
	imgId = id.substr( 6 ),
	state = this.checked,
	data = {
	    match: matchId,
	    state: {
	    }
	};

    /*
    //check if it's t1-place-first or t2-place-first
    if ( this.id == 'check-t1-place-first' )
    {
	//document.getElementById('check-t2-place-first').checked = !(document.getElementById('check-t1-place-first').checked);
	var state2 = !(document.getElementById('check-t1-place-first').checked),
	    imgId2 = 'check-t2-place-first'.substr(6);

	data.state[imgId2] = state2;

	$( '#' + imgId2 ).toggleClass( 'show' );
	$( '#' + imgId2 + '-g' ).toggleClass( 'show' );
    }

    
    if ( this.id == 'check-t2-place-first' )
    {
	//document.getElementById('check-t1-place-first').checked = !(document.getElementById('check-t2-place-first').checked);
	var state2 = !(document.getElementById('check-t2-place-first').checked),
	    imgId2 = 'check-t1-place-first'.substr(6);

	data.state[imgId2] = state2;

	$( '#' + imgId2 ).toggleClass( 'show' );
	$( '#' + imgId2 + '-g' ).toggleClass( 'show' );
    }
    */
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
    var ids = Object.keys( state );

    // We want to find everything that's currently shown EXCEPT
    // those images which are shown *by default* since we're going
    // to check these results against the provided state and hide
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
	$( 'form input[type="submit"]' ).val( 'Link' );
	return false;
    }

    matchId = $( '#match' ).val();
    
    $.get( 'server.php', { match: matchId }, function ( res ) {
	if ( Array.isArray( res ) && !res.length ) {
	    $( '#sync-message' )
		.text( 'Match not found. New match created.' )
		.css( 'color', 'black' )
		.fadeIn( 50 );
	    //this should also "link" it now
	    return;
	}
	updateState( res );
	intervalHandle = setInterval( function () {
	    $.get( 'server.php', { match: matchId }, function ( res ) {
		updateState( res );
	    } );
	}, 250 );

	$( 'form input[type="submit"]' ).val( 'End link' );
	$( '#sync-message' )
	    .text( 'Link established successfully. Now updating every 250ms.' )
	    .fadeIn( 50 )
	    .delay( 3000 )
	    .fadeOut( 1000 );
    } );

    e.preventDefault();
}

$( 'body' ).on( 'change', 'input[type="checkbox"]', onCheckboxChange );
$( '#matchForm' ).on( 'submit', linkMatch );
$( 'body' ).on( 'change', 'input[type="radio"]', onRadioChange );

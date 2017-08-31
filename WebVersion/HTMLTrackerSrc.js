var mapstoneCount1 = 0,
	mapstoneCount2 = 0;

function updateMapstone() {
	// id is of format `mapstone-<t1|t2>-<inc|dec>`
	var id = this.id,
		words = id.split( '-' );

	if ( words[1] === 't1' ) {
		if ( words[2] === 'inc' && mapstoneCount1 < 9 ) {
			mapstoneCount1++;
		} else if ( words[2] === 'dec' && mapstoneCount1 > 0 ) {
			mapstoneCount1--;
		}
		$( '#mapstoneTextT1' ).text( mapstoneCount1 + '/9' );
	} else if ( words[1] === 't2' ) {
		if ( words[2] === 'inc' && mapstoneCount2 < 9 ) {
			mapstoneCount2++;
		} else if ( words[2] === 'dec' && mapstoneCount2 > 0 ) {
			mapstoneCount2--;
		}
		$( '#mapstoneTextT2' ).text( mapstoneCount2 + '/9' );
	}
}

function updateTracker() {
	// id is of format `<t1|t2>-<ID of corresponding image>`
	var id = this.id,
		imgId = id.substr( 6 );

	$( '#' + imgId ).toggleClass( 'show' );
	$( '#' + imgId + '-g' ).toggleClass( 'show' );
}

$( 'button' ).on( 'click', updateMapstone );
$( 'body' ).on( 'change', 'input[type="checkbox"]', updateTracker );

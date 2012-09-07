<?php
/*

  Consume a premium gnip stream, with compression.

*/

$stream_url = "YOUR_STREAM_URL_HERE";
$user = "YOUR_USERNAME_HERE";
$pass = "YOUR_PASSWORD_HERE";

// WRITEFUNCTION callback
// required to return the length of the data passed to it

function print_out_data($ch, $data) {
  echo $data;
  return strlen($data);
}

$ch = curl_init();
curl_setopt_array($ch, array(
  CURLOPT_URL => $stream_url,
  CURLOPT_ENCODING => "gzip",
  CURLOPT_FOLLOWLOCATION => true,
  CURLOPT_HTTPAUTH => CURLAUTH_BASIC,
  CURLOPT_USERPWD => $user.":".$pass,
  CURLOPT_WRITEFUNCTION => "print_out_data",
//  CURLOPT_VERBOSE => true // uncomment for curl verbosity

));

$running = null;

$mh = curl_multi_init();
curl_multi_add_handle($mh, $ch);

// the event loop

do {
  curl_multi_select($mh, 1);      // wait for activity
  curl_multi_exec($mh, $running); // perform activity

  print	'.'; 

} while($running > 0);

curl_multi_remove_handle($mh, $ch);
curl_multi_close($ch);
?>

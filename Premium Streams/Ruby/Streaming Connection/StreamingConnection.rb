require 'rubygems'
require 'curb'

# This uses the 'curb' libcurl wrapper for ruby, found at https://github.com/taf2/curb/  
# Usage: <script> username password url
# prints data to stdout.

# Note: this snippet DOES NOT handle breaking up the chunks of data into separate lines.
# You should take this into account and provide additional functionality to do so.

username,password,url = ARGV.first 3

Curl::Easy.http_get url do |c|
  c.http_auth_types = :basic
  c.username = username
  c.password = password

  c.encoding = "gzip"
  c.verbose = true

  c.on_body do |data|
    puts data
    data.size # required by curl's api.
  end
end

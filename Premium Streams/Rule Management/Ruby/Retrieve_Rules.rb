require 'rubygems'
require 'curb'

# This uses the 'curb' libcurl wrapper for ruby, found at https://github.com/taf2/curb/  
# prints data to stdout.

url = "ENTER_RULES_API_URL_HERE"

Curl::Easy.http_get url do |c|
  c.http_auth_types = :basic
  c.username = "ENTER_USERNAME_HERE"
  c.password = "ENTER_PASSWORD_HERE"
  c.encoding = "gzip"
  c.verbose = true # Modify to false to limit output to only JSON rule payload 

  c.on_body do |data|
    puts data
    data.size # required by curl's api.
  end
end
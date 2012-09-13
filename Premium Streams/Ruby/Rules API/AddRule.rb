require 'rubygems'
require 'curb'

# This uses the 'curb' libcurl wrapper for ruby, found at https://github.com/taf2/curb/  
# prints data to stdout.

url = "ENTER_RULES_API_URL_HERE"
rule_value = "ENTER_RULE_VALUE_HERE"
rule_tag = "ENTER_RULE_TAG_HERE"
rule = "{\"rules\":[{\"value\":\"" + rule_value + "\",\"tag\":\"" + rule_tag + "\"}]}"


Curl::Easy.http_post(url) do |c|
  c.http_auth_types = :basic
  c.username = "ENTER_USERNAME_HERE"
  c.password = "ENTER_PASSWORD_HERE"
  c.post_body = rule
  c.verbose = true

  c.on_body do |data|
    puts data
    data.size # required by curl's api.
  end
end

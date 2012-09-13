#!/usr/bin/env python

import urllib2
import base64
import json
import sys

def post():
        url = 'ENTER_RULES_API_URL_HERE'
	UN = 'ENTER_USERNAME_HERE'
	PWD = 'ENTER_PASSWORD_HERE'
	rule = 'ENTER_RULE_VALUE_HERE'
	tag = 'ENTER_RULE_TAG_HERE'

	values = '{"rules": [{"value":"' + rule + '","tag":"' + tag + '"}]}'
	base64string = base64.encodestring('%s:%s' % (UN, PWD)).replace('\n', '')
	req = urllib2.Request(url=url, data=values)
	req.add_header('Content-type', 'application/json')
	req.add_header("Authorization", "Basic %s" % base64string)  
	response = urllib2.urlopen(req)
	the_page = response.read()

if __name__ == "__main__":
        post()

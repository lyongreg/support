#!/usr/bin/env python

import urllib2
import base64
import zlib
import threading
from threading import Lock
from cStringIO import StringIO
import json
import sys

# tune these as needed
CHUNKSIZE = 1024
MAXBUFFSIZE = 20*CHUNKSIZE

NEWLINE = '\r\n'

url = "YOUR_STREAM_URL_HERE"
UN = 'YOUR_USERNAME_HERE'
PWD = 'YOUR_PASSWORD_HERE'

print_lock = Lock()

class procEntry(threading.Thread):
	def __init__(self, buf):
		self.bufList = [x.strip() for x in buf.split(NEWLINE) if x.strip() <> '']
		threading.Thread.__init__(self)

	def run(self):
		try:
			self.output(self.bufList)
		except Exception, e:  # v hard to debug, catches everything! Replace to make more helpful.
			sys.stderr.write("thread failed: (%s)\n"%e)

	def output(self, bufList):
		for rec in bufList:
			jrec = json.loads(rec.strip())
			with print_lock:
				print(json.dumps(jrec))

def get():
    headers = {
    'Accept': 'application/json',
    'Connection': 'keep-alive',
    'Accept-Encoding' : 'gzip',
    'Authorization' : 'Basic %s' % base64.encodestring('%s:%s' % (UN, PWD))
    }
    req = urllib2.Request(url, headers=headers)
    response = urllib2.urlopen(req)
    
    d = zlib.decompressobj(16+zlib.MAX_WBITS)

    ldata = StringIO()    	
    bufSize = 0
    while True:
	tmpString = d.decompress(response.read(CHUNKSIZE))
	bufSize += len(tmpString)
    	ldata.write(tmpString)
	if tmpString.endswith(NEWLINE) and bufSize > MAXBUFFSIZE:
		procEntry(ldata.getvalue()).start()
		ldata = StringIO()
		bufSize = 0

if __name__ == "__main__":
    get()

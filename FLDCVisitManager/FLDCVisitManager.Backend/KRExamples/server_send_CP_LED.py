# sm, 21.01.2020

import requests
import time
import json

headers = {'content-type': 'application/json'}
serverUrl = "http://192.168.15.49:8080/"  # trailing '/' is mandatory

# send LED sequence
payload = {'pattern': [0xFF0000, 0x00FF00, 0x0000FF, 0x00],
           'timer': [500, 500, 500, 500],
           'cycles': 2,
           'seqName': 'RGB',
           }
print("Data: " + json.dumps(payload))
r = requests.post(serverUrl + "setLedColorSequence", data=json.dumps(payload), headers=headers)
print("Status: " + str(r.status_code))
print("Body: " + r.text)

time.sleep(10)

# send single color
payload = {'color': 0xFFFF00 }
print("Data: " + json.dumps(payload))
r = requests.post(serverUrl + "setLedColor", data=json.dumps(payload), headers=headers)
print("Status: " + str(r.status_code))
print("Body: " + r.text)

time.sleep(5)

# reload stored LED sequence
payload = {'seqName': 'RGB',
           'cycles': 5,
           }
print("Data: " + json.dumps(payload))
r = requests.post(serverUrl + "setLedColorSequence", data=json.dumps(payload), headers=headers)
print("Status: " + str(r.status_code))
print("Body: " + r.text)

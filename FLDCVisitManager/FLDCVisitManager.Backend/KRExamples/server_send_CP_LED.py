# sm, 21.01.2020

import requests
import time
import json

headers = {'content-type': 'application/json'}
clientUrl = "http://192.168.20.180:8080/"  # trailing '/' is mandatory

# send LED sequence
payload = {'pattern': [0xFF0000, 0x00FF00, 0x0000FF, 0x00],
           'timer': [500, 500, 500, 500],
           'cycles': 2,
           'seqName': 'RGB',
           'run': False,
           }
print("Data: " + json.dumps(payload))
r = requests.post(clientUrl + "setLedColorSequence", data=json.dumps(payload), headers=headers)
print("Status: " + str(r.status_code))
print("Body: " + r.text)

# send LED sequence
payload = {'pattern': [0xFFFFFF, 0x00],
           'timer': [500, 500 ],
           'cycles': 0,
           'seqName': 'BlinkWhite',
           'run': True,
           }
print("Data: " + json.dumps(payload))
r = requests.post(clientUrl + "setLedColorSequence", data=json.dumps(payload), headers=headers)
print("Status: " + str(r.status_code))
print("Body: " + r.text)

time.sleep(5)

# send single color
payload = {'color': 0xFFFF00 }
print("Data: " + json.dumps(payload))
r = requests.post(clientUrl + "setLedColor", data=json.dumps(payload), headers=headers)
print("Status: " + str(r.status_code))
print("Body: " + r.text)

time.sleep(1)

# reload stored LED sequence
payload = {'seqName': 'RGB',
           'cycles': 5,
           }
print("Data: " + json.dumps(payload))
r = requests.post(clientUrl + "setLedColorSequence", data=json.dumps(payload), headers=headers)
print("Status: " + str(r.status_code))
print("Body: " + r.text)

time.sleep(15)

# reload stored LED sequence
##payload = {'seqName': 'RAINBOW',
##           'cycles': 2,
##           }
##print("Data: " + json.dumps(payload))
##r = requests.post(clientUrl + "setLedColorSequence", data=json.dumps(payload), headers=headers)
##print("Status: " + str(r.status_code))
##print("Body: " + r.text)

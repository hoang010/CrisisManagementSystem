import random
import names
import string

loginUrl = 'http://localhost:59460/Login/Authorize'
loginUrlShort = '/Login/Authorize'

loginInfo = {'Username': 'admin',
             'Password': 'password',
             'name': 'Login'}

crisesListUrl = 'http://localhost:59460/Crises'
crisesListUrlShort = '/Crises'
createCrisesUrl = 'http://localhost:59460/Crises/Create'
createCrisesUrlShort = '/Crises/Create'

headers = {
    'Content-Type': 'application/x-www-form-urlencoded',
}


def randomTextGenerator(x):
    username = ''.join([random.choice(string.ascii_lowercase) for n in range(x)])
    return username


def phoneGenerate():
    phone = ''.join([random.choice(string.digits) for n in range(7)])
    phone = '8' + phone
    return phone

def locationGenerator():
    locations = ['WEST', 'EAST', 'SOUTH', 'NORTH', 'CENTRAL']
    location = locations[random.randint(0, 4)]
    return location


def create_crises_info(verification_token):
    crises_info = {'Callername': names.get_full_name(),
                   'CallerNumber': phoneGenerate(),
                   'Location': locationGenerator(),
                   'Description': randomTextGenerator(20),
                   'AssistanceRequiredId': 3,
                   'CategoryId': 3,
                   'EmergencyId': 3,
                   '__RequestVerificationToken': verification_token
                   }
    return crises_info


CREATE = 'Create'
DELETE = "Delete"
EDIT = "Edit"

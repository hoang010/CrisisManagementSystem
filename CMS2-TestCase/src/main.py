from requests_html import HTMLSession

from Constants import *

session = HTMLSession()

session.post(loginUrl, loginInfo)


def get_verification_token(r):
    verification_token = r.html.find('input', first=True).attrs['value']
    return verification_token


def create_crises():
    r = session.get(createCrisesUrl)
    verification_token = get_verification_token(r)
    payload = create_crises_info(verification_token)
    return_action(CREATE, payload, createCrisesUrl)


def return_action(action, payload, url):
    session.post(url, data=payload, headers=headers)


def get_crises(properties):
    r = session.get(crisesListUrl)
    links = [x for x in r.html.absolute_links if properties in x]
    return links


def delete_crises():
    links = get_crises(DELETE)
    links.sort(key=lambda x: int(x.split("/")[-1]))
    for url in links:
        r = session.get(url)
        verification_token = get_verification_token(r)
        payload = {'__RequestVerificationToken': verification_token}
        return_action(DELETE, payload, url)
        print("Deleted {}".format(url))


def edit_crises():
    links = get_crises(EDIT)
    for url in links:
        r = session.get(url)
        verification_token = get_verification_token(r)
        payload = create_crises_info(verification_token)
        return_action(EDIT, payload, url)

# for i in range(10):
#     create_crises()
while True:
    delete_crises()

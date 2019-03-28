import re
import random
from locust import HttpLocust, TaskSet, task

from Constants import *


class UserBehavior(TaskSet):
    @staticmethod
    def get_verification_token(r):
        verification_token = re.search('value=.*\"', r.text)[0].split("=")[1].split(" ")[0].replace("\"", '')
        return verification_token

    @task(4)
    def login(self):
        self.client.post(loginUrlShort, loginInfo)

    @task(4)
    def create_post(self):
        r = self.client.get(createCrisesUrlShort)
        verification_token = UserBehavior.get_verification_token(r)
        # print(verification_token)
        # self.verification_token = verification_token
        payload = create_crises_info(verification_token)
        r = self.client.post(createCrisesUrl, data=payload, headers=headers)
        if r.status_code != 200:
            print(r.text)
        else:
            print("Created")

    @task(1)
    def delete_post(self):
        r = self.client.get(crisesListUrlShort)
        links = re.findall("/Crises/Delete/[0-9]*", r.text)
        link = links[random.randint(0, links.__len__() - 1)]
        # base = 'http://localhost:59460' + link
        r = self.client.get(link)
        verification_token = UserBehavior.get_verification_token(r)
        payload = {'__RequestVerificationToken': verification_token}
        r = self.client.post(link, data=payload, headers=headers)
        if r.status_code != 200:
            print(r.text)
        else:
            print("deleted {}".format(link))

    @task(1)
    def edit_post(self):
        r = self.client.get(crisesListUrlShort)
        links = re.findall("/Crises/Edit/[0-9]*", r.text)
        link = links[random.randint(0, links.__len__() - 1)]
        # base = 'http://localhost:59460' + link
        r = self.client.get(link)
        verification_token = UserBehavior.get_verification_token(r)
        payload = create_crises_info(verification_token)
        r = self.client.post(link, data=payload, headers=headers)
        if r.status_code != 200:
            print(r.text)
        else:
            print("edited {}".format(link))



class WebsiteUser(HttpLocust):
    task_set = UserBehavior

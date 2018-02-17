import requests
from bs4 import BeautifulSoup
import os
import sys
import os.path
from tkinter import *
import tkinter.messagebox

# Create a new file


def write_file(path,data):
    f=open(path, 'w')
    f.write(data)
    f.close()

# Add data into an existing file


def append_to_file(path, data):
    with open(path, 'a') as file:
        file.write(data + '\n')


def trade_spider():

    try:
        # Get file name and url address

        url = urlEntry.get()
        file_name = fileNameEntry.get()

        text_file = file_name + '.txt'

        links_set = set()

        source_code = requests.get(url)
        plain_text = source_code.text
        soup = BeautifulSoup(plain_text, "html.parser")

        # Add links from the site to links_set

        for link in soup.findAll('a'):
            href = link.get('href')
            if 'http' in href:
                links_set.add(href)
            else:
                href = url + '/' + link.get('href')
                links_set.add(href)

        # Check if file exists

        if os.path.isfile(text_file) is False:
            write_file(text_file, "")

        # Add all links to text file

        for link in links_set:
            append_to_file(text_file, link)
        root.quit()
    except:
        tkinter.messagebox.showinfo("Error", "Wrong URL address")

# User interface


root = Tk()
fileNameEntry = StringVar()
urlEntry = StringVar()

root.geometry("600x250")
root.title("Web Crawler")


infoLabel = Label(root, text="URL address - url address of page you want to gather all links from\n "
                             "File name - name of text file with gathered links", pady=10).pack()


urlLabel = Label(root, text="Url address:").pack()
urlEntry = Entry(root,textvariable=urlEntry, width=50)
urlEntry.pack()
fileNameLabel = Label(root, text="File name:", pady=10).pack()
fileNameEntry = Entry(root, textvariable=fileNameEntry)
fileNameEntry.pack()


startButton = Button(root, text="Start", command=trade_spider, pady=30, padx=40).pack()


root.mainloop()






# _Shoe Stores_

#### _C# advanced databases code review, 3/4/2016_

#### By _**David Diehr**_

## Description

This is a very simple web app that would allow a user to keep and update a database of shoe stores and shoe brands as well as the relationship between them.

## Setup/Installation Requirements

* _Clone this repository_
* _Open the Windows Powershell and connect to your local database_
* _In SQLCMD:_
* _1>CREATE DATABASE shoe\_stores;_
* _2>GO_
* _1>USE shoe\_stores;_
* _2>GO_
* _1>CREATE TABLE stores (id INT IDENTITY(1,1), name VARCHAR(255));_
* _2>CREATE TABLE brands (id INT IDENTITY(1,1), name VARCHAR(255);_
* _3>CREATE TABLE stores_brands (id INT IDENTITY(1,1), store_id INT, brand_id INT);_
* _4>GO_
* _1>Quit_
* _In the Windows Powershell navigate to the project folder_
* _and run the following commands:_
* _dnu restore_
* _dnx kestrel_
* _Then open the browser of your choice to localhost:5004_


## Known Bugs

_None._


## Technologies Used

_C# with the Nancy web framework; SQL Server 2016 CTP3; HTML; xUnit for testing_

### Legal

Copyright (c) 2016 **_David Diehr_**

This software is licensed under the MIT license.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.


# Learning Hub

## Introduction
        
This is ASP .NET Core MVC based web application. In this application user can enroll/disenroll for any training which is conducted by trainer and trainer can add tranings. Each trainer has an option for edit and remove their trainings. Two views has been provided one is calender view and other one is table view. In both of view the authorized user can do the modification.

For authentication purpose, token based authentication is used. Role of admin user is provided through which admin can add/delete/update users or trainings. MS SQL database is used. Api is used to fetch/update data from database.     


# Pages

## Login Page

Single login page for all users(trainer,trainee,admin). Each user has different level of authorization. Trainee can only enroll/disenroll for trainings. Trainer and add/update/remove his own trainings. Admins can add/update/remove trainers or trainee or training.

**Workflow:** After user submits its credentials an api is called to send credentials. Credentials is verified at backend and if user is found it returns a token. This token is used internally for any subsequent request made by user. 

## Dashboard Page

#### Trainee Dashboard Page

Trainee can see ongoing trainings in tabular as well as calender format. Trainee can enroll/disenroll for trainings.

<img src="https://github.com/harshalkolhe0/Learning-Hub/blob/main/Images/TraineePage.png?raw=true" width="700"  height = "400">


#### Trainer Dashboard Page



#### Admin Dashboard Page

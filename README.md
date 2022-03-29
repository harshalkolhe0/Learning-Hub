
# Learning Hub

## Introduction
        
This is ASP .NET Core MVC based web application. In this application user can enroll/disenroll for any training which is conducted by trainer and trainer can add tranings. Each trainer has an option for edit and remove their trainings. Two views has been provided one is calender view and other one is table view. In both of view the authorized user can do the modification.

For authentication purpose, token based authentication is used. Role of admin user is provided through which admin can add/delete/update users or trainings. MS SQL database is used. Api is used to fetch/update data from database.     


## Pages

#### Login Page

Single login page for all users(trainer,trainee,admin). Each user has different level of authorization. Trainee can only enroll/disenroll for trainings. Trainer and add/update/remove his own trainings. Admins can add/update/remove trainers or trainee or training.

**Workflow:** After user submits its credentials an api is called to send credentials. Credentials is verified at backend and if user is found it returns a token. This token is used internally for any subsequent request made by user. 


#### Trainee Dashboard Page

Trainee can see ongoing trainings in tabular as well as calender format. Trainee can enroll/disenroll for trainings.

<img src="https://github.com/harshalkolhe0/Learning-Hub/blob/main/Images/TraineePage.png?raw=true" width="700"  height = "400">

When Trainee click on enroll button, a toast is appear on top right corner which shows successfullly enrolled and enroll button changes to disenroll. 

<img src="https://github.com/harshalkolhe0/Learning-Hub/blob/main/Images/Enroll.png?raw=true" width="700"  height = "400">

#### Trainer Dashboard Page

Trainer includes trainee feature and can modify,create,delete his own trainings.

<img src="https://github.com/harshalkolhe0/Learning-Hub/blob/main/Images/TrainerPage.png?raw=true" width="700"  height = "400">

If trainer click on add a course button then below page appears with current date and time:

<img src="https://github.com/harshalkolhe0/Learning-Hub/blob/main/Images/CourseUpdatedView.png?raw=true" width="700"  height = "400">

If trainer click on edit button the below page appears:

<img src="https://github.com/harshalkolhe0/Learning-Hub/blob/main/Images/CreateView.png?raw=true" width="700"  height = "400">

After Saving update the toast appear at top left corner.

<img src="https://github.com/harshalkolhe0/Learning-Hub/blob/main/Images/CourseUpdatedView.png?raw=true" width="700"  height = "400">

In calender view, trainer click on his training below page appears:

<img src="https://github.com/harshalkolhe0/Learning-Hub/blob/main/Images/SelfTrainings.png?raw=true" width="700"  height = "400">

On clicking Edit below page appears:

<img src="https://github.com/harshalkolhe0/Learning-Hub/blob/main/Images/CalenderSaveView.png?raw=true" width="700"  height = "400">

On saving is shows:

<img src="https://github.com/harshalkolhe0/Learning-Hub/blob/main/Images/CalenderUpdatedView.png?raw=true" width="700"  height = "400">

If trainer opens a training which he doesn't own then it shows :

<img src="https://github.com/harshalkolhe0/Learning-Hub/blob/main/Images/OthersTrainings.png?raw=true" width="700"  height = "400">

#### Admin Dashboard Page

Admin includes trainee feature and can modify,create,delete any trainings.

<img src="https://github.com/harshalkolhe0/Learning-Hub/blob/main/Images/AdminPage.png?raw=true" width="700"  height = "400">

Admin feature also include add/update/delete users.

<img src="https://github.com/harshalkolhe0/Learning-Hub/blob/main/Images/UserView.png?raw=true" width="700"  height = "400">


## Api

Api is secured with token based authentication. Without authentication it shows unauthorized:

<img src="https://github.com/harshalkolhe0/Learning-Hub/blob/main/Images/Api.png?raw=true" width="700"  height = "400">

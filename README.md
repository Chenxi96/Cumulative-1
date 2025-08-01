# Cumulative 1

Checklist:

(Quantitative: 8 Marks): Implement the required MVP using ASP.NET Core:
- [x] (2 Marks) An API which adds a Teacher
- [x] (2 Marks) An API which deletes a Teacher
- [x] (2 Marks) A web page that allows a user to enter new Teacher information
- [x] (2 Marks) A web page that confirms the action to delete a Teacher
(Qualitative: 8 Marks): Document your work with (a) descriptive:
- [ ] (2 Marks) Summary blocks for your API methods
- [ ] (2 Marks) Teacher Model Properties (Teacher.cs)
- [ ] (2 Marks) Variable names
- [x] (2 Marks) Project .readme
(Testing: 8 Marks): Include evidence of the following testing:
- [ ] (2 Marks) Your API that adds a Teacher using POST Data
- [x] (2 Marks) Your API that deletes a Teacher
- [ ] (2 Marks) Your web page that allows a user to enter new Teacher information
- [x] (2 Marks) Your web page that confirms the action to delete a Teacher
(Initiative: 8 Marks): Earn up to 8 Initiative Marks by improving on MVP
- [ ] (2 Marks) Error Handling on Delete when trying to delete a teacher that does not exist
- [x] (2 Marks) Error Handling on Add when the Teacher Name is empty
- [ ] (2 Marks) Error Handling on Add when the Teacher Hire Date is in the future
- [ ] (2 Marks) Error Handling on Add when the Employee Number is not “T” followed by digits1
- [ ] (2 Marks) Error Handling on Add when the Employee Number is already taken by a different Teacher
- [x] (2 Marks) Add Functionality Students
- [x] (2 Marks) Delete Functionality for Students
- [x] (2 Marks) Add Functionality Courses
- [x] (2 Marks) Delete Functionality for Courses
- [ ] (2 Marks) Use JS and AJAX to add a Teacher
- [ ] (2 Marks) Use JS and AJAX to delete a Teacher
- [x] (2 Marks) Modify Teachers table with “teacherworkphone” field, include that field as part of the Add functionality

1 Best practice would be to have the Teacher Employee Number managed by the database as a unique
identifier, similar to the Teacher Id primary key. Not part of this assignment
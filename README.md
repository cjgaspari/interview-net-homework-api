This project has been completed by CJ Gaspari. It fulfills the requirements for the endpoints requested for students and teachers. 


Both student and teacher endpoints require authorization. Stub code is added to simulate authorization. 

To test Teacher endpoints, use the following: admin:admin, teacher:password

To test Student endpoints, use any username:password combo. Assignments submitted will be associated with username. 

These are the following endpoints:
POST: /Assignment/student/submit
DELETE: /Assignment/teacher/{assignmentId}
​GET: /Assignment​/teacher​/getAssignments
​GET: /Assignment​/student​/getAssignments
PUT: /Assignment/teacher/updateGrade

Additionally, endpoints were made to allow multiple comments and attachments.
GET: /Attachment/{id}
POST: /Attachment
DELETE: /Attachment/teacher/{id}

POST: /Note/teacher/addNote
GET: /Note/{id}
DELETE: /Note/teacher/{id}

Models: 

Assignment:
{
id	integer
assignmentName	string
nullable: true
studentName	string
nullable: true
submissionTime	date-time
gradingTime	date-time
attachments	[...]
grade	string
nullable: true
notes	[...]
}

Attachment:
{
id	integer
assignmentId	integer
attachmentUrl	string
}

Note:
{
id	integer
assignmentId	integer
note string
}



### Objective

Your objective is to implement a simple API for a school's homework submission platform that enables students to submit their homework and teachers to grade students' submissions.

### Brief

Using **C#** and **.Net**, your challenge is to build a simple API for a school's homework submission platform. You are expected to design any required models and routes for your API and document your endpoints in a separate markdown file, "ENDPOINTS.md".

### Tasks

-   Implement the assignment using:
    -   Language: **C#**
    -   Framework: **.Net**
-   There should be API routes that allow students to:
-   Submit their homework
-   View their homework submissions
    -   Filter by grade (A - F, incomplete, ungraded)
    -   Filter by assignment name
-   There should be API routes that allow teachers to:
    -   See an overview of all homework submissions
        -   Filter by assignment name, date range (to - from), and individual student name
    -   Grade individual homework submissions (A-F, comments)
-   Add unit tests for your business logic

Each homework object should minimally include the following fields:

-   Assignment name
-   Student name
-   Submission datetime
-   Grading datetime
-   File attachment (pdf/jpeg)
-   Final grade
-   Teachers Notes

### Evaluation Criteria

-   C# best practices
-   Completeness: Did you include all features?
-   Correctness: Does the solution perform in a logical way?
-   Maintainability: Is the solution written in a clean, maintainable way?
-   Testing: Has the solution been adequately tested?
-   Documentation: Is the API well-documented?

### CodeSubmit

Please organize, design, test, and document your code as if it were going into production - then push your changes to the master branch. After you have pushed your code, you must submit the assignment via the assignment page.

All the best and happy coding,

The PDS Team
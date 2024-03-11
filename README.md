## taskograph
Program that measures personal tasks (eg. reading, running, working, cooking) duration on each day. Provides charts of your efficiency (eg how much time you've spent reading in a day/week/month). Helps to focus on goals and maintain pace of productivity.

## Concept UI

Navigation Bar:
1)  Task Mode   -> Main place of the program, where you measure activities you do during day.
2)  Targets     -> View specific targets dates you have set up so you know how much time you have left to complete it. Or view compleated tasks to celebrate your achievements.
3)  Graphs      -> See charts of your productivity in the last day, week, month, year.
4)  Motivation  -> Get motivated to work harder by reading some inspirational quotes or thoughts, celebrating your achivements.
5)  Settings    -> Set up list of tasks, groups, and alarm clock to take a break during work.

# Task Mode
<img width="611" alt="image" src="https://github.com/lucasadamski/taskograph/assets/12997783/bcb2ecbf-b10c-4dfb-a02f-4f057c51d883">

Sections: 
1) Current date
2) Current task and timer - shows your current task and how much time you're at it.
3) Start/stop button - Start or Stop measuring time of current task.
4) All tasks today timer - summary of total work done for today.
5) Manual entry - Enables you add/edit/delete duration of tasks for past days or current day.
7) Add/remove tasks - add or remove tasks from current list so you can focus only on fewer things.
8) Task table: Shows Group Name, Task Name, Start Button, Add Custom Time, Total For Today, MoveUp/Down Buttons
    * Group - each task belongs to Group, eg. Tasks Running, Cooking, Walking might belong to Health Group.
    * Task Name - activity type you want to measure, eg. Running, Reading
    * Start - Start Button to start measuring the time while you at the task
    * Add Time - Insted of measuring the time you can Add fixed amount of time at once
    * Total Today - shows how much of task you've done today
    * MoveUp/Down Buttons - move task on up/down within table so you can prioritize it 
9) Time Targets Per Day - reminds you of resolutions of how much time you want to certain task each day
10) Updoming Date Targets - keeps in your mind upcoming deadlines you decided to complete

# Task Mode Options
<img width="611" alt="image" src="https://github.com/lucasadamski/taskograph/assets/12997783/6a7ad772-2068-4398-9b9f-f6fe4943102b">

# Targets
<img width="611" alt="image" src="https://github.com/lucasadamski/taskograph/assets/12997783/2b12d9dd-84e8-4f8c-a7ec-274a2c4233c9">

Sections: 
1) Add New - add new Target
2) Filtering data:
     * Status - show compleated only / in-progress only / all
     * Date - date range
     * Clear Filters - cancels filters and shows all Targets
4) Target List - in each row shows Group, Task, Description of Target, Date Added, Date Due (and how much time allocated from add date was), Status (how much time left / Compleated), Actions (Edit/Delete Target)  

# Graphs
<img width="611" alt="image" src="https://github.com/lucasadamski/taskograph/assets/12997783/9624ca0a-b068-4c52-9371-1bbacce11751">
<img width="611" alt="image" src="https://github.com/lucasadamski/taskograph/assets/12997783/665829bb-cbc3-4156-a578-7e61da6b0f54">
<img width="611" alt="image" src="https://github.com/lucasadamski/taskograph/assets/12997783/2e2c36be-5b5c-4884-b457-1e4e6218709b">

Description:
You can see your productivity in 3 chart styles: Text, Line or Bar Graph. Next choose which time frame you want to see results (per day, week, month or year). Finally check which Tasks/Groups you want to be included in a Graph. 

Select Task:
1) Single Task/Group
2) Multiple Tasks/Group
3) WARNING: Can't check Task AND Group  - it's only Task OR Group

Graph Styles:
1) Text 
2) Line 
3) Bar  

Time Frames:
1) Day   - you get results for that day only. 
2) Week  - you get each day of the week, and a summary of the whole week.
3) Month - each week of the month, and summary of the whole month.
4) Year  - each month of the year, and summary of the whole year.

# Motivation
<img width="611" alt="image" src="https://github.com/lucasadamski/taskograph/assets/12997783/4e50cba9-8194-49aa-9e17-005498e2ad41">

Description:
Get motivated with inspirational quotes, get reminded how much targets you've achieved and get hyped up to work harder!

# Settings
<img width="611" alt="image" src="https://github.com/lucasadamski/taskograph/assets/12997783/b11f7af7-850f-40ad-a3f1-400f433847c0">

Decription:
Add Tasks, Groups. Set Alarm Beep every Time Interval to take a break from work. 


## Database 
<img width="611" alt="image" src="https://github.com/lucasadamski/taskograph/assets/12997783/58d0ae83-3626-4f52-9dfe-4ec1eb6f44b3">

1) Task Group
    * Task belongs to a only one Group. Group contains many Tasks.
    * Entry belongs to only one Task. Every day will have one Entry for one Task, so for example if user was doing task "Reading" 3 times per day for 10 minutes each,  this will be merged to one Entry "Reading" "30 minutes". The exact time of day doesn't matter. Day is the fundamental unit of time in this program.

2) Target Group
    * Precise_Target - eg. "Read Little Prince by 30th of March 2023"
    * Regular_Target - eg "Read for 10 minutes everyday"

3) Settings Group
      * Settings - Bool, Time values for Alarm Clock. The rest is Option Ids that store string values.
      * Option - eg. "TaskListOrder" value:"Id2_Id0_Id5" - stores order Tasks in Task Mode Table
4) Quotes Group
      * Standalone Quote for Motivational Section





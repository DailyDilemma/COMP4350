2016-01-19 21:27
Matt Deutscher
I added a couple of the big user stories to the GitHub repo as issues, seems the best way to track and assign them. Take a look, and if no one objects, I'll add the rest the same way.

2016-01-19 21:42
Brent Rempel
Looks good to me!

2016-01-19 22:01
Daniel J. Bouchard
Hey guys, could you please put the github stuff up here so I can find it. Alternatively you can add Airistotal to the project. The vision statement is done, do you guys want it on the markdown for our github?

2016-01-19 22:08
Matt Deutscher
https://github.com/DailyDilemma/COMP4350

2016-01-19 22:09
Daniel J. Bouchard
Cool, thanks

2016-01-19 22:42
Daniel J. Bouchard
I created a pill request for you guys to look at seeing as how I don't have access to the repo.
*pull

2016-01-20 01:56
Josh Westlake
You named the group COMP4350 Project Discussion.

2016-01-20 01:58
Josh Westlake
FYI I'm exporting our chat logs to our GitHub repository as per the assignment so just a friendly reminder - don't say anything in here from this point forward that you wouldn't want made public :).

2016-01-20 03:00
Josh Westlake
I just took a look at the sample vision statements we got in COMP3350 and I think our vision statement is missing a couple of things. We need to say under what circumstances we would consider our system a success, such as how many downloads, a certain rating in the app store, a certain userbase size or some tangible measure of how it helped people. Also, I think we need to talk about a scenario in which we see people using the system, (such as the grocery list scenario) and describe what they do now and how we see our app improving what they do now with respect to an actual use case. I'll touch it up later if possible, I just wanted to throw this out there in case someone else feels what I'm getting at and wants to look at it before then. I'll post my sample from last term shortly so you can see what I mean.

2016-01-20 03:01
Josh Westlake
*photo or sticker*

2016-01-20 03:01
Josh Westlake
*photo or sticker*

2016-01-20 10:28
Matt Deutscher
Do we want a link to the Trello board on our overview page in GitHub? Then the prof can view it as well...

2016-01-20 10:29
Daniel J. Bouchard
We had discussed we weren't going to, but I don't really care either way.

2016-01-20 10:50
Matt Deutscher
cool, won't bother then.

2016-01-20 11:09
Matt Deutscher
I'm taking a stab at adding a few things to the vision statement, will be done soon.

2016-01-20 11:09
Daniel J. Bouchard
Cool, go for it.

2016-01-20 11:55
Matt Deutscher
Just noticed in the chat logs, the link to join the Trello board works. Do we want to delete it?

2016-01-20 11:57
Josh Westlake
Sure. I'm not too concerned about security but if we've all joined the board already it doesn't need to be made public.

2016-01-20 12:00
Matt Deutscher
ok. It's gone.

2016-01-20 12:46
Josh Westlake
I think things look good, our submission for iteration 0 is done. Nothing left to worry about except the bazillion other assignments we all have to do :P

2016-01-20 18:32
Mitch Lenton
Whoa, didn't realize that a separate chat was started. I was just going to ask who was supposed to hand in the itr0. Question answered =D

2016-01-26 00:30
Josh Westlake
I have a lunch appointment tomorrow so I can't make it to class or our regularly scheduled meeting. This won't happen very often, hopefully all of you can still get together at least briefly and discuss any concerns or outline what you feel should happen next. I've been trying to get the basic project architecture done so that even the .Net newbies can make sense of things from here but it's taking me longer than I'd like, if the other people with .Net experience want to take the architecture in another direction I'm fine with that too.

2016-01-28 12:38
Brent Rempel
Hey guys, I thought I would be able to make it to the university in time for the end of class but I won't. I spent some time looking at webAPI last night and I think I have a handle on how the front end is working. I'm going to spend some time tonight looking at entity framework so that this weekend I can help with getting the project going.

2016-01-29 16:06
Josh Westlake
For anyone who hasn't checked, we got 15/15 on Iteration 0, feedback is in GitHub https://github.com/DailyDilemma/COMP4350/blob/master/docs/Grades/Group_H_Iteration0_Marking.pdf

2016-01-29 16:25
Daniel J. Bouchard
*photo or sticker*

2016-01-31 14:26
Josh Westlake
I updated the project a couple days ago so it has very basic abilities to view, add, edit and delete lists. We have a lot of clean up to do yet but at we can see something working.
I strongly encourage anyone who adds something to project based on what they read in an online resource to add a link to that resource in the project wiki. That way everyone else who encounters the code at least has some idea what it does.

2016-01-31 14:30
Josh Westlake
To that end, anyone who doesn't understand how the project currently works should follow this tutorial http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application which basically explains step by step how to create a similar project.

2016-01-31 14:33
Mitch Lenton
Thanks Josh! I'll take a look at that in a bit. I've spent the last day familiarizing myself with asp.net again (been awhile).

2016-01-31 17:40
Daniel J. Bouchard
Fabulous. I should be starting to work on that soon.

2016-01-31 19:52
Brent Rempel
Thanks a lot Josh!

2016-02-03 20:19
Brent Rempel
Hey guys, I pulled down master and tried to run the app and I got a error. Anyone else getting this?

2016-02-03 20:20
Brent Rempel
*photo or sticker*

2016-02-03 20:22
Josh Westlake
I haven't looked at it recently but try right clicking the ListAssist project and selecting "set as startup project"

2016-02-03 20:26
Brent Rempel
That did the trick. Thank you.

2016-02-04 14:59
Daniel J. Bouchard
Hey guys, while trying to run the project it is saying that ListAssist.WebAPI.MappingProfile doesn't exist. It's causing errors. I led in the repo and it doesn't have those files either. The shopping list object also can not be found. Any ideas?

2016-02-04 15:34
Mark Cortilet
Let me double check that vs didn't exclude those from my commit

2016-02-04 15:49
Daniel J. Bouchard
Sounds good.

2016-02-04 15:49
Mark Cortilet
Yeah that looks like what happened

2016-02-04 15:49
Daniel J. Bouchard
Lovely, thanks vs.

2016-02-04 15:53
Mark Cortilet
K just pushed the files again

2016-02-04 16:06
Daniel J. Bouchard
Awesome. That fixed a lot. Now I'm getting an error on ValuesController

2016-02-04 16:06
Mark Cortilet
That shouldn't be there

2016-02-04 16:24
Daniel J. Bouchard
Hmmmm. I'm also getting a bunch of warnings about missing XML components for the items in the ListAssist. WebAPI

2016-02-04 16:31
Mark Cortilet
Those don't seem to matter

2016-02-04 16:31
Daniel J. Bouchard
Lovely. Well, I'll try to copy the repo down again and see if something bad happened there. I'll let you guys know how it goes.

2016-02-04 16:57
Mitch Lenton
I've finished up the unit and integration tests for now (I'm sure more could be added later) and I have issued a pull request. If everything looks good then it can be merged.

2016-02-04 17:38
Daniel J. Bouchard
Well, I'm still getting all the same errors as before. I've tried what was suggested before and I set ListAssist as the startup project
The first thing that goes wrong is an SQLException that isn't being caught. When I do catch it, what is opened up is a welcome page for foundation.

2016-02-04 17:43
Josh Westlake
I'm trying to look into it, lots of interruptions here at the moment. I'm not getting the same errors you're describing, but I am getting errors.

2016-02-04 17:43
Daniel J. Bouchard
The next error is in ListAssist.WebAPI.Tests/controllers/ValueControllerTests.cs

2016-02-04 17:43
Josh Westlake
I'm going to resolve the errors I have first, then merge the pull requests, resolve any errors there and once that happens you should be good to pull something down again.

2016-02-04 17:44
Daniel J. Bouchard
It's just ValuesController is not defined

2016-02-04 17:44
Josh Westlake
Ah, okay. That's what I have.
Working on it.

2016-02-04 17:44
Mitch Lenton
I believe the SQLException error is caused by the entity framework missing. Check the solution packages,

2016-02-04 17:45
Daniel J. Bouchard
Sorry to be a bother, but what am I checking for exactly?

2016-02-04 17:48
Mitch Lenton
Right click on the solution and select manage nuget packages for solution and check that Microsoft.AspNet.Identity.EntityFramework is installed for the project. Click on it, then check off the project checkbox to the right.

2016-02-04 17:49
Daniel J. Bouchard
Fabulous, it wasn't installed.
I'm not sure if I should push the changes that was made by the install...

2016-02-04 17:52
Josh Westlake
The issue with the ValuesController happened because we moved around some functionality after the unit tests were created and now the unit tests are trying to call something that has been moved. This might be a good time to remind everyone to actually run the unit tests prior to pushing anything (I'm guilty of it too on occasion)

2016-02-04 17:54
Josh Westlake
I'll fix it and merge everything and get it up to speed shortly. Daniel don't bother pushing anything until you get my latest changes from master in the next little while. See if that fixes anything before we add more stuff to it.

2016-02-04 17:55
Daniel J. Bouchard
Sounds good.

2016-02-04 20:45
Josh Westlake
When you get a second pull down the latest master and see how it works for you Daniel.

2016-02-04 21:12
Josh Westlake
FYI I've disabled direct pushes to the master branch to encourage people to create new branches / pull requests for future work. I can turn it off if you want but what I'm trying to do is to get everyone in habit of making branches instead of direct pushing. The nice thing about a branch is its very easy to pull someone's work to your machine and inspect it BEFORE it hits the master branch and we find out it breaks it for everyone else. If you hate that idea and still want to direct push I believe you all have authority to turn it off under "Settings" in the GitHub page.

2016-02-04 21:20
Mitch Lenton
I'm fine with that idea. It gives time to verify that the branch will work properly with the master branch.

2016-02-04 22:00
Brent Rempel
Yeah I'm also good with that. It's definitely a good practice to have.

2016-02-04 22:08
Josh Westlake
@Mark I've had absolutely no luck getting the swagger stuff to work. When you get a moment could you branch off master and see if you can get it working again? It's missing files again, I'm not sure where they went though.

2016-02-04 22:09
Mark Cortilet
Mmk

2016-02-04 22:16
Mark Cortilet
Just created a branch off origin/master and it seemed alright
which files is it saying it's missing?

2016-02-04 22:19
Josh Westlake
Oh right sorry, I guess that got fixed as I was moving everything around. The issue that still exists is that when I click "Swagger UI" in the web page for that project I get this...

2016-02-04 22:19
Josh Westlake
*photo or sticker*

2016-02-04 22:21
Mark Cortilet
think i know what it is. another file didn't get committed
you have all the dlls and whatnot?

2016-02-04 22:23
Josh Westlake
I believe so, I think somewhere along the lines a controller called swagger might have gotten lost somewhere

2016-02-04 22:23
Mark Cortilet
close

2016-02-04 22:27
Daniel J. Bouchard
Hey, still got the errors for values controller

2016-02-04 22:28
Mark Cortilet
k, committed what i think you were missing
it's in branch swagger-test2

2016-02-04 22:30
Josh Westlake
k. Mark, were you able to run the project without any issues when you pulled down origin/master just now?
Oh, and I think you still need to push your branch up Mark, I don't see it yet :)

2016-02-04 22:32
Mark Cortilet
oh woops

2016-02-04 22:32
Daniel J. Bouchard
I'm looking in the repo and there is no branch there.

2016-02-04 22:35
Mark Cortilet
so apparently it won't let me push that branch

2016-02-04 22:38
Josh Westlake
I wonder if you left this option clicked when you branched...

2016-02-04 22:38
Josh Westlake
*photo or sticker*

2016-02-04 22:38
Josh Westlake
If so you created a different branch locally, but remotely it's tracking master and would attempt to push up into master

2016-02-04 22:39
Mark Cortilet
that would be it

2016-02-04 22:47
Josh Westlake
i think the easiest way to get around that is to branch off your local branch so you have a new (untracked) local branch with a different name. Then go to "Sync" and under Outgoing click on "Publish" and that should make a new remote branch.

2016-02-04 22:59
Mark Cortilet
so at some point, all the files i committed got removed from my local repo, and i can't seem to get them back

2016-02-04 23:00
Daniel J. Bouchard
Oh no :-(

2016-02-04 23:02
Mark Cortilet
fortunately, they're still in the master repo

2016-02-04 23:30
Mark Cortilet
ok, think i sorted it out. new branch is SwaggerAPI

2016-02-04 23:41
Josh Westlake
Just trying it out, looks good so far

2016-02-04 23:41
Mark Cortilet
phew
Github UI is awesome

2016-02-04 23:42
Josh Westlake
you mean their desktop app?

2016-02-04 23:42
Mark Cortilet
yeah
I couldn't fix the problem through vs

2016-02-04 23:43
Josh Westlake
ya I find it makes more sense than visual studios in a lot of cases, and the nice thing is once you figure it out you never have to learn how to use another IDE plugin again... no matter what technology stack you use the GitHub UI stays the same.

2016-02-04 23:46
Matt Deutscher
thank god. I thought I was the only one that was having a hard time with VS GitHub integration.

2016-02-04 23:48
Brent Rempel
Yeah I definitely switched over to the github ui

2016-02-04 23:57
Mitch Lenton
I gave it a run and everything seems to be working.

2016-02-05 00:01
Josh Westlake
k, thanks Mitch. Still curious why it won't run for Daniel, good to know it's working for someone else.

2016-02-05 00:05
Matt Deutscher
detailed user stories for iteration 2 are up on GitHub. Please, feel free to critique them. I'll change whatever anyone wants...

2016-02-05 00:40
Daniel J. Bouchard
No errors for me guys.

2016-02-05 00:41
Mitch Lenton
Great! I made a quick change to the integration tests as three were failing due to the database not being re-initialized. All unit tests now work.

2016-02-05 00:57
Daniel J. Bouchard
Kool. I've gotten as far as to have no errors, but when I start the project it just shows a blank page. I'm going to bed now and I'll continue trying to get my end to work later.

2016-02-09 17:05
Josh Westlake
No meeting tonight, family stuff came up

2016-02-09 17:07
Matt Deutscher
OK.

2016-02-10 01:52
Josh Westlake
Okay, remote meeting today (Wed) @ 7:30pm. Here's what you need to know:
- Prior to the meeting I will post a link in our group chat. Use *Chrome* to navigate to the link. (Only chrome offers full support for all meeting features without an additional download.)
- If you can't make it no problem, we can move the time a little if need be or break into smaller meetings for those who missed it
- A mic is optional if you prefer to type but you should have a pair of headphones so you can hear those of us who are talking

2016-02-10 01:52
Josh Westlake
The rest we'll sort out at the meeting

2016-02-10 01:53
Daniel J. Bouchard
*photo or sticker*

2016-02-10 08:15
Mitch Lenton
That works for me.

2016-02-10 10:46
Brent Rempel
Sounds good

2016-02-10 19:13
Josh Westlake
https://join.me/276-236-567

2016-02-10 19:15
Josh Westlake
You might not see my screen yet but you should be able to join the lobby. I won't actually start anything until 7:30

2016-02-10 19:29
Josh Westlake
Seems we have our first issue...
Or well, maybe not so much. In order to hear me you'll have to "call in" to the audio conversation whether you on talking or not.

2016-02-10 19:30
Daniel J. Bouchard
Be there in a sec.

2016-02-10 19:31
Matt Deutscher
Kids are being difficult. I'll be there in a few minutes

2016-02-10 19:34
Josh Westlake
K, other than you 2 the rest of us are here and things seem to be working

2016-02-10 20:01
Mitch Lenton
Assuming that Amazon reactivates my account soon, then I'll go through and get things setup and see about adding everyone as admins.

2016-02-10 20:01
Josh Westlake
Okay, sounds good.

2016-02-11 19:15
Josh Westlake
Reminder I'll be doing a tutorial presentation at 8pm for whoever's available.

2016-02-11 19:58
Josh Westlake
https://join.me/693-133-407

2016-02-11 20:02
Josh Westlake
Mark or Dan, you joining us? You don't have to I'm just curious

2016-02-11 20:03
Daniel J. Bouchard
Sorry, my girlfriend is making me fix her comp atm.

2016-02-11 20:04
Mark Cortilet
hammering away at os still

2016-02-11 20:04
Josh Westlake
k

2016-02-11 21:34
Josh Westlake
If anyone is trying to recreate something done in the tutorial, you can checkout the remote branch "jw-Tutorial-20160211" or if you just want to try and add the changes manually you view the individual changes online at https://github.com/DailyDilemma/COMP4350/compare/master...jw-Tutorial-20160211

2016-02-11 21:34
Josh Westlake
Grrrr, why does it have to post my face everytime...

2016-02-11 21:35
Matt Deutscher
because you're the awesomeist
thanks again. reading up on razor syntax, dataannotations, and entity fluent api...

2016-02-12 10:37
Mitch Lenton
I've re-activated my AWS account and have setup access accounts for you all. These accounts give full access to Amazon EC2 and Amazon Electric Beanstalk. If there are other services that I should grant access to let me know. In the mean time I'll message each of you individually with your initial login credentials. You'll have to change your password when you first login.

2016-02-12 12:25
Matt Deutscher
cool, thks

2016-02-12 13:45
Brent Rempel
Sounds good

2016-02-13 11:26
Josh Westlake
With regards to that email the instructor sent out, I added usage instructions and emailed him that they've been added should his markers want to get started. Also, he has officially added the Iteration 2 requirements which I've posted in a Trello card and broken down into easy to read checklists we can follow https://trello.com/c/hqPSWMaS/66-iteration-2-requirements-checklist

2016-02-13 11:27
Josh Westlake
Oh and as always, friendly reminder: 13 days left

2016-02-13 11:30
Mitch Lenton
Just an update on AWS, I'll need to sort some stuff out as it seems to be charging me for usage. So I will sort this out before we host the app.

2016-02-13 11:32
Josh Westlake
Guess it's hard to become a multi-billion dollar company without stealing a few pennies from the little guy here and there :P

2016-02-13 11:32
Mitch Lenton
Indeed. :)

2016-02-13 11:50
Mitch Lenton
What DB are we going to use for the app?

2016-02-13 12:11
Josh Westlake
Stick with sql server

2016-02-13 12:11
Mitch Lenton
ok

2016-02-17 19:24
Mitch Lenton
Hey all. I've figured out the billing issues with amazon and I am currently attempting to launch an Elastic Beanstalk instance. It has had repeated failures in building the environment but I'll keep going until it succeeds (it usually takes a few tries).

2016-02-18 17:44
Mitch Lenton
Just a quick update on AWS. Apologies for not having it up and ready but there are some issues that I am trying to iron out with it. It keeps failing to properly launch the server environment.

2016-02-20 16:04
Mitch Lenton
The marks for iteration 1 have been posted.

2016-02-21 22:09
Brent Rempel
Hey guys just so you know I did a bit of clean up on the list creation. Nothing really that major. I saw that we also had some other tickets just for basic testing and functionality of our lists so I'll probably keep working on that tomorrow night.

2016-02-21 22:11
Daniel J. Bouchard
Kool. I've gotten so far as to finally get enterprise and the web API is working for me, but not the normal ListAssist. I should have this all figured out by the end of tonight and start contributing by tomorrow.

2016-02-22 00:25
Daniel J. Bouchard
Opa! It works! I feel like it shouldn't have taken me that long to figure it out...

2016-02-22 10:16
Josh Westlake
Hello all, anyone able to take on adding Selenium to the project and setting up a couple automated acceptance tests? Testing is 40% of the marks on this iteration so we'll definitely need that.

2016-02-22 10:22
Josh Westlake
@Mitch, thanks for all the work with AWS. When you get a second send me my log in credentials, I wouldn't mind taking a look at what errors we have at the moment. I have set up several .NET web servers so maybe it's something I recognize.

2016-02-22 10:33
Matt Deutscher
We couldn't get an instance running on his account. I set one up, deployed the app, and can't get it to connect to MySQL. I'll set up your credentials tout suite, see if you have any ideas.http://listassist-env.us-west-2.elasticbeanstalk.com/LALists

2016-02-22 11:36
Matt Deutscher
I can look at adding Selenium.

2016-02-22 16:15
Josh Westlake
The app is functioning on AWS at http://ec2-52-36-187-54.us-west-2.compute.amazonaws.com/
Due to the current architecture of our solution I'll have to set up another website for the WebAPI but I'll do that later. For the moment auto-deploy features don't work, but I'll try to sort that out next iteration. For now I'll just do a manual deploy on release day.

2016-02-22 16:22
Daniel J. Bouchard
*photo or sticker*

2016-02-22 16:47
Matt Deutscher
ok. I killed the Elastic Beanstalk environment.

2016-02-22 18:04
Mitch Lenton
I've created a pull request for the editing and removing of list items. I'll merge if there are no objections.

2016-02-22 18:05
Josh Westlake
just taking a quick peek

2016-02-22 18:05
Mitch Lenton
no problem.

2016-02-22 18:17
Josh Westlake
Works as advertised :) It's been merged. The new X button might need some styling love but we'll see if Brent gets any time for that. :P

2016-02-22 18:18
Mitch Lenton
Great! Next up will be the adding of new items.

2016-02-22 18:52
Mitch Lenton
Just merged the add list item function.

2016-02-22 19:02
Josh Westlake
Good stuff, works as expected. If anyone is looking for something to do the WebAPI will also need to be able to add and delete items just like we can through the interface. Also, we may want to add some business logic to prevent duplicate items, and some unit tests to confirm it works. Entity framework could be used to create a unique index or you could just do a lookup to verify. Either would work.

2016-02-22 19:03
Mark Cortilet
I can do that

2016-02-22 19:04
Josh Westlake
Cool beans. Team H ftw.

2016-02-22 19:24
Mark Cortilet
can't seem to get the current version of master

2016-02-22 19:25
Josh Westlake
How so, do you have any partial uncommitted changes preventing you from checking out another branch?

2016-02-22 19:26
Mark Cortilet
no, was the first thing i checked. it just doesn't fetch anything and the desktop app doesn't list any changes from the last 2 and a half weeks

2016-02-22 19:29
Josh Westlake
Does the history of the repository in the desktop app show mitch's latest changes?

2016-02-22 19:30
Mark Cortilet
no, last change was merging mitch's tests from last iteration

2016-02-22 19:32
Josh Westlake
what happens if you branch off of origin/master?

2016-02-22 19:37
Mark Cortilet
there we go, was just branching off master

2016-02-22 20:36
Mark Cortilet
mmk, can now add/remove/update items properly in web api

2016-02-23 20:33
Brent Rempel
Hey guys I ended up getting stuck on a styling spree so that's merged in there now. I wanted to do more testing on all things regarding making and removing lists but I haven't got to it yet. If anyone else wants to do that they can. If not I'll try to get it done by Friday.

2016-02-23 20:37
Mark Cortilet
I'm doing a bunch of tests for that right now

2016-02-23 20:37
Brent Rempel
Oh awesome!

2016-02-23 22:26
Mark Cortilet
k, finished a bunch of unit tests, and changed things so duplicates aren't added anymore

2016-02-24 01:13
Daniel J. Bouchard
*photo or sticker*

2016-02-24 13:47
Daniel J. Bouchard
Does anyone want to keep the account files that come with the project? If not I'll take them out.

2016-02-24 13:49
Josh Westlake
Do you mean all of the classes that have to do with the template login scripts? If so, then yes remove them. We won't implement ours that way anyways and if we need to see how it works later we can just generate another template project.

2016-02-24 13:50
Daniel J. Bouchard
Sounds good, I'll clean that up then.

2016-02-24 14:53
Brent Rempel
Hey Josh, I'll clean up the buttons tonight

2016-02-24 14:57
Josh Westlake
k, coolness. It seems to work if you move the class inside the action link. Other than that I like what you've done with the place :)

2016-02-24 14:57
Josh Westlake
*photo or sticker*

2016-02-24 20:30
Brent Rempel
Ok the buttons should be good now

2016-02-24 23:13
Daniel J. Bouchard
They're looking good.
I've just finished removing a bunch of stuff and put it in a pull request. If some of you guys want to sign off on the changes that'd be cool.

2016-02-25 18:06
Mitch Lenton
Just letting you all know that I am going to merge the 2 pull requests.

2016-02-25 18:07
Matt Deutscher
OK.

2016-02-25 18:27
Daniel J. Bouchard
*photo or sticker*

2016-02-25 18:27
Mitch Lenton
Everything is merged and nothing broke. (y)

2016-02-25 18:28
Daniel J. Bouchard
Awesome

2016-02-26 15:07
Josh Westlake
Mark your branch for no-duplicates has a bunch of unit tests in it for the WebAPI, are you still working on it or is it ready to be merged in?

2016-02-26 15:09
Mark Cortilet
Can merge

2016-02-26 15:09
Josh Westlake
Okay, thanks

2016-02-26 20:05
Josh Westlake
Is anyone available to update our meeting minutes. We've got a serious-ish problem with our code base that I'm trying to fix and I'm running out of time.

2016-02-26 20:05
Matt Deutscher
What's the problem?

2016-02-26 20:08
Josh Westlake
All of Mark's changes are gone. All his WebAPI updates and all his tests. When I tried to recover his branch and merge it in again it won't let me. I looked it up apparently when something gets reverted it can't be re-merged, it has to be re-reverted. At this stage though re-reverting is about as much work as just manually copying in his branch piecemeal. It's giving me all sorts of issues with the tests though... the problem itself I seem to have working.

2016-02-26 20:09
Josh Westlake
err the *program is working I mean
just not test tests... too many running together and all cancelling each other out

2016-02-26 20:18
Josh Westlake
Alternatively I can do the minutes and someone else can finish out these crazy tests.
err *figure out

2016-02-26 20:19
Matt Deutscher
I can take a look at the tests, if you need a break

2016-02-26 20:21
Josh Westlake
k, does the problem make sense or do you want to join me in a quick meeting so I can show you on my screen?

2016-02-26 20:21
Matt Deutscher
sure, get me up to sped faster

2016-02-26 20:22
Josh Westlake
k, I'll send a link in a couple minutes.
Anyone with git experience is welcome to join the meeting. I may be wrong about the limitations of git here, and if I was that'd be great news

2016-02-26 20:25
Brent Rempel
As far as I know the best option is to revert the revert. But that's theoretical. I've never actually done it

2016-02-26 20:26
Josh Westlake
but what happens if the revert was 20 commits ago?
doesn't it roll you all the way back to that point in time?

2016-02-26 20:27
Josh Westlake
https://join.me/499-356-711

2016-02-26 20:30
Brent Rempel
Hmmm. I thought you could set the commit right after as the parent and then just do the revert as if nothing else has taken place... Or maybe that was just for merges...

2016-02-26 20:32
Mark Cortilet
Take a look at frequency-start. I branched from duplicates to make it

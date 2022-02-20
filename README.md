# UnoTest

Hi!

Here are my implementation.

-------------------------

First of all, my setup:

ryzen 7 3700x
4x8gb DDR4
asus tuf b450m plus gaming
asus radeon rx 580 oc 8gb
thermaltake smart 600w

windows 11 pro build 22000.493
visual studio community 2022 preview 17.2.0 + ReSharper

internet link 300mb

-------------------------

First I cloned the uno platform repository ( https://github.com/unoplatform/uno ) and open.

The solution is very big, I was turned of my virtual memory settings, but to open this solution it was necessary turn on.

Was necessary to install a lot of things in my computer and I spent about 8 hours trying to build all solution, but without success.

So I decided to use uno like a normal client, and I followed the get start guide, install template on visual studio and uno-check.

Using uno-check, a lot of red messages with errors are displayed, so I solved using uno-check fix and a post on github with a custom command (https://github.com/unoplatform/uno/discussions/7833)

But when I updated the uno-check to a dev version, I also needed to update my visual studio, that was on last stable and now are on preview.

All this steps cost about 8 hours of my day.

-------------------------

Now, with a new solution working on UWP initialized by uno template, I started to dev the test.

Its important to point a fews things:
- I not very good with Math
- Today I developer with Xamarin Forms, that is very similar with WPF
- In my computer, the project Wasm and GTK dont build, all others yes

-------------------------

How I thinked to dev this test:

I used SOLID principles to dev.

A view is stateless, who have state are the ViewModel.

All things that View use to draw and render are on View folder.

The timer are on ViewModel.

The view sign a custom EventHandler that emit a DateTime object and this event who trigger the clock factory.

A ClockFactory are responsable to create all UIElements and only have one public method that return a IEnumerable<UIElement> with all canvas object need to add for draw and render the clock.

How the Event as triggered from a worker thread by timer, I used SynchronizationContext to post to UI Thread the call of method that generate all UIElements and add it to canvas object.

The part of math calcs, I made a search on google to find everything that I need to solve this test.

I have a private skill, if I have a goal, I'll do the goal, dont matter with have wall's to thorugh, I'll do it. Was this way that I started to work like a developer 10 years ago and I still doing the same today.

To dev the test I spent about 4 hours.
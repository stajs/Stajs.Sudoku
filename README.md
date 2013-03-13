# Stajs.Sudoku

This was an experiment after reading an [interesting blog post](http://ravimohan.blogspot.in/2007/04/learning-from-sudoku-solvers.html) comparing two different methods of approaching a sudoku solver. The comments descend in to an agile/TDD flame war, but it was enough to make me wonder how I would TDD a solver.

I wasn't that familiar with sudoku before starting and I avoided reading the two different approaches. The end result mirrored Jeffries: I ended up getting bogged down in data structures while not giving sufficient analytical thought to the problem of actually solving the puzzle, before eventually giving up. As it stands, a brute force solver just doesn't cut the mustard.

Of course, this is not to say that you can't combine design up-front with TDD (I'm sure you can), but the conclusion I've drawn is that TDD may not be the best way to drive design.

FWIW, I'm a fan of TDD.

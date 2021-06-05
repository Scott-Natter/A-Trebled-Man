# A Trebled Man

## Navigating to your repo for Windows user


## Navigating to your repo for MacOS user

### Setting up Git LFS:
Git LFS is how we avoid gross binary conflicts. Navigate to the folder that this repo is located on your computer via command line, copy and paste the code below and press Enter:

```
git lfs install
git lfs track "*.obj" "*.psd" "*.png" "*.ai" "*.fbx"
```

### CLI Basics
List Files
```
ls
```

Change directory
```
cd your-folder-here
```

Go up a directory
```
cd ..
```

Go up multiple directories
```
cd ../../
```

Navigate multiple folders
```
cd ../../my-folder-here/subfolder/etc
```

Remove a file
```
rm your-file-here
```

make a folder
```
mkdir new-folder-name
```

### Git Basics

Tell you what is going on. This is your best friend.
```
git status
```

Tells you what has been committed lately. q to quit, arrow keys to scroll.
```
git log
```

Fetch the latest branches from GitLab, remove deleted ones.
```
git fetch -p
```

Show all branches, red ones are on GitLab, white ones are on your machine, green one is your current one.
```
git branch -a
```

Checkout the branch you want to work on.
```
git checkout your-branch-name-here
```

Add a file to be committed
```
git add your-file-here
```

Add all files that you have modified, must be at the root of the repo
```
git add .
```

Commit the files you have added
```
git commit -m "Your message here"
```

Push the committed files to GitLab on your branch
```
git push
```

Pull the latest changes on your current branch
```
git pull
```

I give up, undo all my changes since the last commit (LOSE ALL CHANGES)
```
git reset HEAD --hard
```

I give up, undo all my changes since my last push (LOSE ALL CHANGES)
```
git reset master --hard
```
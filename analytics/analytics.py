from itertools import count
import gspread as gs
import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt
import numpy as np

# Load Google Sheets data into Pandas dataframes
gc = gs.service_account(filename='foxtrot-analytics.json')
did_finish_sheet = gc.open_by_url('https://docs.google.com/spreadsheets/d/1HrI4zVEx_c9PKyrEEFg2Qi0-7PjB1FyprVBOe6f0QYw/edit?resourcekey#gid=1881965523')
portal_use_sheet = gc.open_by_url('https://docs.google.com/spreadsheets/d/1uWWQ7coQKffPQeyn5jsCq6Yql5bWVQF2VBqAhQCD-YY/edit?usp=sharing')
health_sheet = gc.open_by_url('https://docs.google.com/spreadsheets/d/17BnmISvDoqx76OJoc0MhrI1-VB9Xx0b_h_3YYyjmU40/edit?resourcekey#gid=919270586')
enemy_sheet = gc.open_by_url('https://docs.google.com/spreadsheets/d/1-sthW5lMDrmSKu5H8Zf1oSOL-aAarPrSni6j-SYWtO4/edit?resourcekey#gid=1919056539')

did_finish_sheet = did_finish_sheet.worksheet('Form Responses 1')
portal_use_sheet = portal_use_sheet.worksheet('Form Responses 1')
health_sheet = health_sheet.worksheet('Form Responses 1')
enemy_sheet = enemy_sheet.worksheet('Form Responses 1')

did_finish_df = pd.DataFrame(did_finish_sheet.get_all_records())
portal_use_df = pd.DataFrame(portal_use_sheet.get_all_records())
health_df = pd.DataFrame(health_sheet.get_all_records())
enemy_df = pd.DataFrame(enemy_sheet.get_all_records())

did_finish_df.drop('Timestamp', axis=1, inplace=True)
portal_use_df.drop('Timestamp', axis=1, inplace=True)
health_df.drop('Timestamp', axis=1, inplace=True)
enemy_df.drop('Timestamp', axis=1, inplace=True)

print(health_df)
print(enemy_df)

"""
Metric 1: Enemies Encountered
"""
# Add a variable for level 
enemy_df['level'] = 1
enemy_df.reset_index()
print(enemy_df)

# Calculate the average number of enemies encountered and killed by level
enemy_df = enemy_df.groupby(['level'])[['enemies_enocountered', 'enemies_killed']].mean().reset_index()
print(enemy_df)
print(len(enemy_df))

# Create a side-by-side bar plot for 
# Number of pairs of bars
N = len(enemy_df)
ind = np.arange(N)
width = 0.3
plt.bar(ind, tuple(enemy_df['enemies_enocountered']), width, label = 'Enemies Encountered', color = "#6596C7")
plt.bar(ind + width, tuple(enemy_df['enemies_killed']), width, label = 'Enemies Killed', color = "#7fcdbb")
plt.xlabel('Level')
plt.ylabel('Average Number of Enemies')
plt.suptitle('Average Number of Enemies Encountered vs. Killed')
plt.title('By Level')

level_labels = tuple(str(item) for item in tuple(enemy_df['level']))
plt.xticks(ind + width / 2, level_labels)

plt.legend(loc='best')
plt.savefig('enemies_plot.png')
plt.close()

"""
Metric 2: Health of Player
"""
### Plot 1: Average Player Health
### Side-by-side bar plots grouped by level

# Add a variable for level 
health_df['level'] = 1
health_df.reset_index()
print(health_df)

# Calculate the average player health at the end of each level
health_df_avg = health_df.groupby(['level'])['health_level'].mean().reset_index()
print(health_df_avg)
print(len(health_df_avg))

# Create a side-by-side bar plot for 
# Number of pairs of bars
game_level_labels = tuple(str(item) for item in tuple(health_df_avg['level']))
health_level_labels = tuple(health_df_avg['health_level'])
plt.bar(game_level_labels, health_level_labels, width = 0.3, color = "#6596C7")
plt.ylim(0, 1.5)
plt.xlabel('Level')
plt.ylabel('Average Health')
plt.title('Average Player Health at End of Level')

for x,y in zip(game_level_labels,health_level_labels):

    label = "{:.2f}".format(y)

    plt.annotate(label, # this is the text
                 (x,y), # these are the coordinates to position the label
                 textcoords="offset points", # how to position the text
                 xytext=(0,10), # distance from text to points (x,y)
                 ha='center') # horizontal alignment can be left, right or center


plt.legend(loc='best')
plt.savefig('avg_health_plot.png')
plt.close()

### Plot 2: Level 1 Distribution of Player Health
### Vertically stacked bar plots for each level

sns.countplot(x = 'health_level', data = health_df, palette = ["#6596C7"])
plt.title("Player Health Disribution")
plt.xlabel("End of Level Health")
plt.ylabel("Number of Players")
plt.savefig('dist_health_plot.png')
plt.close()

"""
Metric 3: Level Completions
"""
### Plot 1: Count of Completions: Bar
### Side-by-side bar plots grouped by level

did_finish_df['level'] = 1
did_finish_df.reset_index()
print(did_finish_df)
sns.color_palette("hls", 8)
sns.countplot(x ='did_finish', data = did_finish_df, palette= ["#7fcdbb", "#edf8b1"])
plt.title("No of players to finish the level - T or F")
plt.xlabel("Did player finish?")
plt.ylabel("No of players")
plt.savefig('did_finish_bar_plot.png')
plt.close()

### Plot 2: Percentage of Users that Completed Level
### Side-by-side bar plots grouped by level


### Plot 3: Overall Percentage of Completions
### Pie Chart
did_finish_df['count_val'] = 1
did_finish_df_counts = did_finish_df.groupby(['did_finish']).sum().reset_index()

colors = ['#D98D82', '#82D9A8']
fig = plt.figure(figsize =(10, 7))
plt.pie(did_finish_df_counts['count_val'], labels=did_finish_df_counts['did_finish'], autopct='%1.1f%%', colors=colors)
plt.title("Overall Percentage of Level Completions")
plt.savefig('did_finish_pie_plot.png')
plt.close()

"""
Metric 4: Portal Use
"""
portal_use_merge = pd.merge(did_finish_df, portal_use_df, on='sessionID', how="outer")

portal_use_merge['portal_used'] = portal_use_merge['portal_used'].apply(lambda x: "no" if pd.isnull(x) else "yes")
portal_use_merge.drop_duplicates(inplace=True, ignore_index=True)

sns.countplot(x ='portal_used', data = portal_use_merge, palette= ["#7fcdbb", "#edf8b1"])
plt.title("No of players to use the portal")
plt.xlabel("Did player use the portal?")
plt.ylabel("No of players")
plt.savefig('portal_use_plot.png')
plt.close()

# Plots using CSV data
"""
# Load using CSV
local_path = '/Users/munseonghun/Downloads/'
did_finish_df = pd.read_csv(local_path + 'enemy_kills.csv')
portal_use_df = pd.read_csv(local_path + 'Portal_Use.csv')
did_finish_df.drop('Timestamp', axis=1, inplace=True)
portal_use_df.drop('Timestamp', axis=1, inplace=True)

# Metric 1: Did user finish level
ax = sns.countplot(x ='did_finish', data = did_finish_df, palette= ["#7fcdbb", "#edf8b1"])
plt.xlabel('Did player finish?') 
plt.ylabel('No of plays') 
plt.title('No of players to finish the level - T or F')
plt.bar_label(ax.containers[0])
plt.savefig(local_path + 'did_finish_chart.png')
plt.close()

# Metric 2: Did user use a portal
portal_use_merge = pd.merge(did_finish_df, portal_use_df, on='sessionID', how="outer")
portal_use_merge.drop_duplicates(inplace=True, ignore_index=True)
portal_use_merge['portal_used'] = portal_use_merge['portal_used'].apply(lambda x: "no" if pd.isnull(x) else "yes")

ax = sns.countplot(x ='portal_used', data = portal_use_merge, palette= ["#7fcdbb", "#edf8b1"])
plt.xlabel('Did player use the portal?') 
plt.ylabel('No of plays') 
plt.title('No of players to use the portal')
plt.bar_label(ax.containers[0])
plt.savefig(local_path + 'portal_use_chart.png')
plt.close()
"""

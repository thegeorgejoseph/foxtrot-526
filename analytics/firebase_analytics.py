import firebase_admin
from firebase_admin import credentials
from firebase_admin import db
import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt
import numpy as np

cred = credentials.Certificate("/Users/sarapesavento/Documents/foxtrot-analytics-95472-firebase-adminsdk-6e9fo-b1ac3bad32.json")
firebase_admin.initialize_app(cred, {
    'databaseURL': 'https://foxtrot-analytics-95472-default-rtdb.firebaseio.com/'
})
ref = db.reference('metrics')
json_data = ref.get()

"""
Clean Data
"""
# remove '10' key and keep entries that are timestamp : metrics
# Before: del json_data['10']
month_keys = ['10']
clean_data = dict((k, json_data[k]) for k in json_data if k in month_keys)
for key in month_keys:
    clean_data.update(clean_data.pop(key))

clean_data = {key: value for key, value in clean_data.items() if key not in ['5','8']}
day_keys = [key for key in clean_data]
for key in day_keys:
    print(key)
    clean_data.update(clean_data.pop(key))

df_transpose = pd.DataFrame.from_dict(clean_data)
print(df_transpose)
df = df_transpose.T
df = df.reset_index()
print(df)
print(df.columns)

# Remove prefixes from variables
df = df.replace('Level_', '', regex=True)
df['enemies_encountered'] = pd.to_numeric(df['enemies_encountered'])
df['enemies_killed'] = pd.to_numeric(df['enemies_killed'])
df['health'] = pd.to_numeric(df['health'])
df['levelCompletionTime'] = pd.to_numeric(df['levelCompletionTime'])
df['levelScore'] = pd.to_numeric(df['levelScore'])

"""
Metric 1: Level Completions
"""
### Plot 1: Count of Completions: Bar
### Level Completions Counts
df['finished_count'] = 0
df.loc[df['finished'] == 'True','finished_count'] = 1
df['total_finished_count'] = 1
finish_by_level = df.groupby(['level'])[('finished_count', 'total_finished_count')].sum().reset_index()
print(finish_by_level)
plt.bar(finish_by_level['level'], finish_by_level['finished_count'], color = "#6596C7")
plt.xlabel('Level')
plt.ylabel('Count')
plt.title('Successful Level Completions')
# leave room for counts
print('FINISHED COUNT')
print(finish_by_level['finished_count'])
print(max(finish_by_level['finished_count']))
plt.ylim(0, max(finish_by_level['finished_count']) + max(finish_by_level['finished_count'])*.10)
for x,y in zip(finish_by_level['level'],finish_by_level['finished_count']):

    label = "{:.0f}".format(y)

    plt.annotate(label, # this is the text
                 (x,y), # these are the coordinates to position the label
                 textcoords="offset points", # how to position the text
                 xytext=(0,10), # distance from text to points (x,y)
                 ha='center') # horizontal alignment can be left, right or center


plt.legend(loc='best')
plt.savefig('firebase_plots/level_completion_count_bar.png', dpi=1200)
#plt.show()
plt.close()

### Plot 2: Players that completed the level vs. overall
### Line plot
plt.plot(finish_by_level['level'], finish_by_level['finished_count'], label = 'Successful Completions', color='#FFB06B')
for x,y in zip(finish_by_level['level'],finish_by_level['finished_count']):

    label = "{:.0f}".format(y)

    plt.annotate(label, # this is the text
                 (x,y), # these are the coordinates to position the label
                 textcoords="offset points", # how to position the text
                 xytext=(0,-12), # distance from text to points (x,y)
                 ha='center') # horizontal alignment can be left, right or center

plt.plot(finish_by_level['level'], finish_by_level['total_finished_count'], label = 'Total Completions', color='#6596C7')
plt.ylim(0, max(finish_by_level['total_finished_count']) + max(finish_by_level['total_finished_count'])*.10)
for x,y in zip(finish_by_level['level'], finish_by_level['total_finished_count']):

    label = "{:.0f}".format(y)

    plt.annotate(label, # this is the text
                 (x,y), # these are the coordinates to position the label
                 textcoords="offset points", # how to position the text
                 xytext=(0,8), # distance from text to points (x,y)
                 ha='center') # horizontal alignment can be left, right or center

plt.xlabel("Level")
plt.ylabel("Count")
plt.legend()
plt.title('Successful Completions vs. Total Level Plays')
plt.savefig('firebase_plots/level_completion_line_chart.png', dpi=1200)
#plt.show()
plt.close()

### Plot 3: Percentage of Users that Completed Level
### Level Completion Percentages (Success/Overall)
finish_by_level['percentage'] = (finish_by_level['finished_count'] / finish_by_level['total_finished_count']) * 100
print(finish_by_level)
plt.bar(finish_by_level['level'], finish_by_level['percentage'], color = "#6596C7")
plt.xlabel('Level')
plt.ylabel('Percentage')
plt.title('Percentage of Successful Level Completions')
# leave room for counts
plt.ylim(0, max(finish_by_level['percentage']) + max(finish_by_level['percentage'])*.10)
for x,y in zip(finish_by_level['level'],finish_by_level['percentage']):

    label = "{:.2f}%".format(y)

    plt.annotate(label, # this is the text
                 (x,y), # these are the coordinates to position the label
                 textcoords="offset points", # how to position the text
                 xytext=(0,10), # distance from text to points (x,y)
                 ha='center') # horizontal alignment can be left, right or center


plt.legend(loc='best')
plt.savefig('firebase_plots/level_completion_percentage_bar.png', dpi=1200)
#plt.show()
plt.close()

### Plot 4: Compare Count of Successful Completions vs. Overall Plays
### Stacked area chart by count
colors = ['#B8E4FF', '#6596C7']
plt.stackplot(finish_by_level['level'], finish_by_level['finished_count'], finish_by_level['total_finished_count'], labels=['Successful Completions', 'Total Completions'], colors=colors)
plt.legend(loc='upper right')
plt.xlabel('Level')
plt.ylabel('Count')
plt.title('Successful Completions vs. Total Level Plays')
plt.savefig('firebase_plots/level_completion_area_chart.png', dpi=1200)
#plt.show()
plt.close()

### Plot 5: Overall Percentage of Completions
### Pie Chart
df['count_val'] = 1
did_finish_df_counts = df.groupby(['finished']).sum().reset_index()
did_finish_df_counts.loc[did_finish_df_counts['finished'] == 'True', 'finished'] = 'Wins'
did_finish_df_counts.loc[did_finish_df_counts['finished'] == 'False', 'finished'] = 'Losses'

colors = ['#ED9C9C', '#97DEB1']
plt.pie(did_finish_df_counts['count_val'], labels=did_finish_df_counts['finished'], autopct='%1.1f%%', colors=colors)
plt.title('Total Game Plays: Wins vs. Losses')
plt.savefig('firebase_plots/level_completion_pie.png', dpi=1200)
#plt.show()
plt.close()

### Plot 6: Total Game Plays
### Bar plot
plt.bar(finish_by_level['level'], finish_by_level['total_finished_count'], color = "#FFB06B")
plt.xlabel('Level')
plt.ylabel('Count')
plt.title('Total Game Plays')
# leave room for counts
plt.ylim(0, max(finish_by_level['total_finished_count']) + max(finish_by_level['total_finished_count'])*.10)
for x,y in zip(finish_by_level['level'],finish_by_level['total_finished_count']):

    label = "{:.0f}".format(y)

    plt.annotate(label, # this is the text
                 (x,y), # these are the coordinates to position the label
                 textcoords="offset points", # how to position the text
                 xytext=(0,10), # distance from text to points (x,y)
                 ha='center') # horizontal alignment can be left, right or center


plt.legend(loc='best')
plt.savefig('firebase_plots/level_game_plays.png', dpi=1200)
plt.close()

"""
Metric 2: Portal Use
"""
### Plot 1: Portal Use Counts for Completed Levels
df_portals = df[['level', 'portalUsageCount']]
df_portals=df_portals.dropna()
print('df_portals')
print(df_portals)
portals_by_level = df_portals.astype({'portalUsageCount': int, 'level': str})
portals_by_level = portals_by_level.groupby(['level'])[('portalUsageCount')].sum().reset_index()
# Change variable types
print('portals_by_level')
print(portals_by_level)
plt.bar(portals_by_level['level'], portals_by_level['portalUsageCount'], color = "#6596C7")
plt.xlabel('Level')
plt.ylabel('Count')
plt.title('Portal Use Counts')
# leave room for counts
print(portals_by_level['portalUsageCount'])
print(max(portals_by_level['portalUsageCount']))
plt.ylim(0, max(portals_by_level['portalUsageCount']) + max(portals_by_level['portalUsageCount'])*.10)
for x,y in zip(portals_by_level['level'],portals_by_level['portalUsageCount']):

    label = "{:.0f}".format(y)

    plt.annotate(label, # this is the text
                 (x,y), # these are the coordinates to position the label
                 textcoords="offset points", # how to position the text
                 xytext=(0,10), # distance from text to points (x,y)
                 ha='center') # horizontal alignment can be left, right or center


plt.legend(loc='best')
plt.savefig('firebase_plots/portal_use_bar.png', dpi=1200)
plt.close()

### Plot 2: Users who did not use portals vs. those who did
portals_by_level = df_portals.astype({'portalUsageCount': int, 'level': str})
print(portals_by_level)
portals_by_level['count_0'] = 0
portals_by_level.loc[portals_by_level['portalUsageCount'] == 0,'count_0'] = 1
portals_by_level['count_non0'] = 0
portals_by_level.loc[portals_by_level['portalUsageCount'] > 0,'count_non0'] = 1
portals_by_level = portals_by_level.reset_index()
portals_by_level = portals_by_level.groupby(['level'])[('count_0', 'count_non0')].sum().reset_index()

plt.bar(portals_by_level['level'], portals_by_level['count_0'], color='#FFB06B', label = '0 Portal Uses')
for x,y in zip(portals_by_level['level'],portals_by_level['count_0']):

    label = "{:.0f}".format(y)

    plt.annotate(label, # this is the text
                 (x,y), # these are the coordinates to position the label
                 textcoords="offset points", # how to position the text
                 xytext=(0,0), # distance from text to points (x,y)
                 ha='center') # horizontal alignment can be left, right or center

plt.bar(portals_by_level['level'], portals_by_level['count_non0'], bottom=portals_by_level['count_0'], color='#6596C7', label = '1+ Portal Uses')
for x,y in zip(portals_by_level['level'],portals_by_level['count_non0']):

    label = "{:.0f}".format(y)

    plt.annotate(label, # this is the text
                 (x,y), # these are the coordinates to position the label
                 textcoords="offset points", # how to position the text
                 xytext=(0,0), # distance from text to points (x,y)
                 ha='center') # horizontal alignment can be left, right or center


plt.legend(loc='best')

portals_by_level['max_count'] = portals_by_level['count_0'] + portals_by_level['count_non0']
plt.ylim(0, max(portals_by_level['max_count']) + max(portals_by_level['max_count'])*.10)

plt.xlabel("Level")
plt.ylabel("Count")
plt.legend()
plt.title('No Portal Use vs. 1+ Portal Uses')
plt.savefig('firebase_plots/portal_use_stacked_bar.png', dpi=1200)
#plt.show()
plt.close()

### Plot 3: Portal Use numbers
portal_counts = df_portals.astype({'portalUsageCount': int, 'level': str})
portal_counts['counts'] = 1
portal_counts = portal_counts.groupby(['level', 'portalUsageCount'])['counts'].sum().reset_index()
colors = ['#B8E4FF', '#6596C7', '#ED9C9C', '#97DEB1', '#FFB06B']

fig, (ax1, ax2, ax3, ax4, ax5) = plt.subplots(1, 5, figsize=(20,20)) 

portal_counts_1 = portal_counts[portal_counts['level'] == '1']
portal_counts_1['sum'] = sum(portal_counts_1['counts'])
labels = portal_counts_1['portalUsageCount']
values = portal_counts_1['counts']
ax1.pie(values,labels = labels, colors = colors, autopct = '%1.1f%%') #plot first pie
ax1.set_title('Level 1')

portal_counts_2 = portal_counts[portal_counts['level'] == '2']
portal_counts_2['sum'] = sum(portal_counts_2['counts'])
labels = portal_counts_2['portalUsageCount']
values = portal_counts_2['counts']
ax2.pie(values,labels = labels, colors = colors, autopct = '%1.1f%%') #plot second pie
ax2.set_title('Level 2')

portal_counts_3 = portal_counts[portal_counts['level'] == '3']
portal_counts_3['sum'] = sum(portal_counts_3['counts'])
labels = portal_counts_3['portalUsageCount']
values = portal_counts_3['counts']
ax3.pie(values,labels = labels, colors = colors, autopct = '%1.1f%%') #plot third pie
ax3.set_title('Level 3')

portal_counts_4 = portal_counts[portal_counts['level'] == '4']
portal_counts_4['sum'] = sum(portal_counts_4['counts'])
labels = portal_counts_4['portalUsageCount']
values = portal_counts_4['counts']
ax4.pie(values,labels = labels, colors = colors, autopct = '%1.1f%%') #plot fourth pie
ax4.set_title('Level 4')

portal_counts_5 = portal_counts[portal_counts['level'] == '5']
portal_counts_5['sum'] = sum(portal_counts_5['counts'])
labels = portal_counts_5['portalUsageCount']
values = portal_counts_5['counts']
ax5.pie(values,labels = labels, colors = colors, autopct = '%1.1f%%') #plot fifth pie
ax5.set_title('Level 5')

plt.suptitle('Number of Portal Uses')
plt.savefig('firebase_plots/portal_use_pie.png', dpi=1200)
plt.close()

"""
Metric 3: Enemies Encountered
"""
def enemies_encountered(df):
    print('function start')
    # Calculate the average number of enemies encountered and killed by level
    df_enemy = df[['level', 'enemies_encountered', 'enemies_killed']]
    print(df_enemy)
    print(type(df_enemy['enemies_encountered'][0]))
    df_enemy = df_enemy.dropna()
    df_enemy = df_enemy.groupby(['level'])[['enemies_encountered', 'enemies_killed']].mean().reset_index()
    print(df_enemy)
    print(len(df_enemy))
    # Create a side-by-side bar plot  
    # Number of pairs of bars
    N = len(df_enemy)
    ind = np.arange(N)
    width = 0.3
    print('test')
    plt.bar(ind, tuple(df_enemy['enemies_encountered']), width, label = 'Enemies Encountered', color = "#6596C7")
    plt.bar(ind + width, tuple(df_enemy['enemies_killed']), width, label = 'Enemies Killed', color = "#7fcdbb")
    plt.xlabel('Level')
    plt.ylabel('Average Number of Enemies')
    plt.suptitle('Average Number of Enemies Encountered vs. Killed')
    plt.title('By Level')

    level_labels = tuple(str(item) for item in tuple(df_enemy['level']))
    plt.xticks(ind + width / 2, level_labels)
    print('test')
    plt.legend(loc='best')
    plt.savefig('firebase_plots/enemies_bar_plot.png')
    plt.close()

enemies_encountered(df)

"""
Metric 4: Health of Player
"""
### Plot 1: Average Player Health
### Side-by-side bar plots grouped by level

# Calculate the average player health at the end of each level
health_master_df_avg = df.groupby(['level'])['health'].mean().reset_index()
print(health_master_df_avg)
print(len(health_master_df_avg))

# Create a side-by-side bar plot for 
# Number of pairs of bars
game_level_labels = tuple(str(item) for item in tuple(health_master_df_avg['level']))
health_level_labels = tuple(health_master_df_avg['health'])
plt.bar(game_level_labels, health_level_labels, color = "#6596C7")
plt.ylim(0, max(health_master_df_avg['health']) + max(health_master_df_avg['health'])*.10)
plt.xlabel('Level')
plt.ylabel('Average Health')
plt.title('Average Player Health at End of Level')
#threshold = 0
#plt.axhline(y=threshold,linewidth=1, color='k')

for x,y in zip(game_level_labels,health_level_labels):

    label = "{:.2f}".format(y)

    plt.annotate(label, # this is the text
                 (x,y), # these are the coordinates to position the label
                 textcoords="offset points", # how to position the text
                 xytext=(0,10), # distance from text to points (x,y)
                 ha='center') # horizontal alignment can be left, right or center


plt.legend(loc='best')
plt.savefig('firebase_plots/health_avg_bar_plot.png')
plt.close()

### Plot 2: Side by Side Distribution of Player Health

df['health_count'] = 1
health_counts = df.groupby(['level', 'health'])['health_count'].sum().reset_index()
print('health_counts')
print(health_counts)
level_1_health = health_counts[health_counts['level'] == '1']
level_1_health_y = list(level_1_health['health_count'])
level_2_health = health_counts[health_counts['level'] == '2']
level_2_health_y = list(level_2_health['health_count'])
health_lables = list(level_2_health['health'])
health_lables = (str(x) for x in health_lables)
print('health_lables')
print(x for x in health_lables)

fig = plt.figure()
ax = fig.add_subplot(111)

print('level_1_health_y')
print(level_1_health_y)
print('level_2_health_y')
print(level_2_health_y)

N = len(level_1_health)
ind = np.arange(N)  
width = 0.27 
rects1 = ax.bar(ind, level_1_health_y, width, color='#FFB06B')
N = len(level_2_health)
ind = np.arange(N)  
rects2 = ax.bar(ind+width, level_2_health_y, width, color='#6596C7')

ax.set_ylabel('Scores')
ax.set_xticks(ind+width)
ax.set_xticklabels(health_lables)
ax.legend( (rects1[0], rects2[0]), ('Level 1', 'Level 2') )

def autolabel(rects):
    for rect in rects:
        h = rect.get_height()
        ax.text(rect.get_x()+rect.get_width()/2., 1.05*h, '%d'%int(h),
                ha='center', va='bottom')

autolabel(rects1)
autolabel(rects2)
max_health = max(level_1_health_y + level_2_health_y)
plt.ylim(0, max_health + max_health*.10)
plt.xlabel('Health')
plt.ylabel('Count')
plt.title('Health At Level End')

plt.savefig('firebase_plots/health_bar_plot.png')
plt.close()

"""
Metric 5: Time taken
"""
### Plot 1: Average time taken take per level
time_by_level = df.groupby(['level'])[('levelCompletionTime')].mean().reset_index()
print(time_by_level)
plt.bar(time_by_level['level'], time_by_level['levelCompletionTime'], color = "#6596C7")
plt.xlabel('Level')
plt.ylabel('Time (seconds)')
plt.title('Average Time Taken to Complete Level')
print(time_by_level['levelCompletionTime'])
print(max(time_by_level['levelCompletionTime']))
plt.ylim(0, max(time_by_level['levelCompletionTime']) + max(time_by_level['levelCompletionTime'])*.10)
for x,y in zip(time_by_level['level'],time_by_level['levelCompletionTime']):

    label = "{:.0f}".format(y)

    plt.annotate(label, # this is the text
                 (x,y), # these are the coordinates to position the label
                 textcoords="offset points", # how to position the text
                 xytext=(0,10), # distance from text to points (x,y)
                 ha='center') # horizontal alignment can be left, right or center


plt.legend(loc='best')
plt.savefig('firebase_plots/time_bar.png', dpi=1200)
#plt.show()
plt.close()

### Plot 2: Time taken vs enemies killed/encountered
#time taken vs enemies killed/encountered
#time taken vs highscore
#enemies killed vs highscore
#health at end vs highscore

"""
Metric 6: Score
"""
### Plot 1: Average score per level
score_by_level = df.groupby(['level'])[('levelScore')].mean().reset_index()
print(score_by_level)
plt.bar(score_by_level['level'], score_by_level['levelScore'], color = "#6596C7")
plt.xlabel('Level')
plt.ylabel('Score')
plt.title('Average Score by Level')
print(score_by_level['levelScore'])
print(max(score_by_level['levelScore']))
plt.ylim(0, max(score_by_level['levelScore']) + max(score_by_level['levelScore'])*.10)
for x,y in zip(score_by_level['level'],score_by_level['levelScore']):

    label = "{:.0f}".format(y)

    plt.annotate(label, # this is the text
                 (x,y), # these are the coordinates to position the label
                 textcoords="offset points", # how to position the text
                 xytext=(0,10), # distance from text to points (x,y)
                 ha='center') # horizontal alignment can be left, right or center


plt.legend(loc='best')
plt.savefig('firebase_plots/score_avg_bar.png', dpi=1200)
#plt.show()
plt.close()

### Plot 2: Score distribution
df['level_score_count'] = 1
score_all = df.groupby(['levelScore'])[('level_score_count')].sum().reset_index()
plt.bar(score_all['levelScore'], score_all['level_score_count'], color = "#6596C7", width = 50)
plt.xlabel('Score')
plt.ylabel('Count')
plt.title('Score Distribution')
plt.ylim(0, max(score_all['level_score_count']) + max(score_all['level_score_count'])*.10)
for x,y in zip(score_all['levelScore'],score_all['level_score_count']):

    label = "{:.0f}".format(y)

    plt.annotate(label, # this is the text
                 (x,y), # these are the coordinates to position the label
                 textcoords="offset points", # how to position the text
                 xytext=(0,10), # distance from text to points (x,y)
                 ha='center') # horizontal alignment can be left, right or center


plt.legend(loc='best')
plt.savefig('firebase_plots/score_bar.png', dpi=1200)
#plt.show()
plt.close()

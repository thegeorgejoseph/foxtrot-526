import gspread as gs
import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt
import numpy as np

# Load using CSV
local_path = '/Users/munseonghun/Downloads/'
did_finish_df = pd.read_csv(local_path + 'enemy_kills.csv')
portal_use_df = pd.read_csv(local_path + 'Portal_Use.csv')


sns.countplot(x ='did_finish', data = did_finish_df, palette= ["#7fcdbb", "#edf8b1"])
plt.xlabel('Did player finish?') 
plt.ylabel('No of plays') 
plt.title('No of players to finish the level - T or F')
plt.savefig(local_path + 'did_finish_chart.png')

sns.countplot(x ='portal_used', data = portal_use_df, palette= ["#7fcdbb", "#edf8b1"])
plt.xlabel('Did player use the portal?') 
plt.ylabel('No of plays') 
plt.title('No of players to use the portal')
plt.savefig(local_path + 'portal_use_chart.png')

# Load using Google Forms
"""
gc = gs.service_account(filename='foxtrot-analytics.json')
did_finish_sheet = gc.open_by_url('https://docs.google.com/spreadsheets/d/1HrI4zVEx_c9PKyrEEFg2Qi0-7PjB1FyprVBOe6f0QYw/edit?resourcekey#gid=1881965523')
portal_use_sheet = gc.open_by_url("https://docs.google.com/spreadsheets/d/1uWWQ7coQKffPQeyn5jsCq6Yql5bWVQF2VBqAhQCD-YY/edit?usp=sharing")

did_finish_sheet = did_finish_sheet.worksheet('Form Responses 1')
portal_use_sheet = portal_use_sheet.worksheet('Form Responses 1')

did_finish_df = pd.DataFrame(did_finish_sheet.get_all_records())
portal_use_df = pd.DataFrame(portal_use_sheet.get_all_records())
did_finish_df.drop('Timestamp', axis=1, inplace=True)
portal_use_df.drop('Timestamp', axis=1, inplace=True)

# print(did_finish_df)
#did_finish_df.drop_duplicates(inplace=True, ignore_index=True)
print(did_finish_df)
sns.color_palette("hls", 8)
sns.countplot(x ='did_finish', data = did_finish_df, palette= ["#7fcdbb", "#edf8b1"])
plt.title("No of players to finish the level - T or F")
plt.xlabel("Did player finish?")
plt.ylabel("No of players")
plt.savefig('did_finish_chart.png')
# plt.show()

# sns.countplot(x='country', data=csv.loc[df["country"].isin(s))) # option2
portal_use_merge = pd.merge(did_finish_df, portal_use_df, on='sessionID', how="outer")

portal_use_merge['portal_used'] = portal_use_merge['portal_used'].apply(lambda x: "no" if pd.isnull(x) else "yes")
portal_use_merge.drop_duplicates(inplace=True, ignore_index=True)

sns.countplot(x ='portal_used', data = portal_use_merge, palette= ["#7fcdbb", "#edf8b1"])
plt.title("No of players to use the portal")
plt.xlabel("Did player use the portal?")
plt.ylabel("No of players")
plt.savefig('portal_use_chart.png')
"""

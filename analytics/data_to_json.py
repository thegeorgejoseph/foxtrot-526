from itertools import count
import gspread as gs
import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt
import numpy as np

# Load Google Sheets data into Pandas dataframes
gc = gs.service_account(filename='foxtrot-analytics.json')
master_sheet = gc.open_by_url('https://docs.google.com/spreadsheets/d/1kLuixeZApgd2pH9_NH4nJU2XvfMmd7NdkVaFAj9-zHk/edit?usp=sharing')
master_sheet = master_sheet.worksheet('Form Responses 1')

master_df = pd.DataFrame(master_sheet.get_all_records())

master_df.drop('Timestamp', axis=1, inplace=True)

print(master_df)

master_df['count_finished'] = 1

# count number of false per level and number of true
test = master_df.groupby(['finished_level', 'level']).sum().reset_index()
print(test)
#finished_level_dict = {'Level_0': ['finished_level']}
master_df = master_df.groupby(['level'])[['enemies_enocountered', 'enemies_killed']].mean().reset_index()

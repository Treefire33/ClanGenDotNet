import os
import re

for name in os.listdir("."):
	if ".py" in name:
		continue
	newName = name.replace("_normal", "").replace("unavailable", "disabled")
	newName = re.sub(r'_hover\b', "_hovered", newName)
	print(newName)
	os.rename("./"+name, "./"+newName)
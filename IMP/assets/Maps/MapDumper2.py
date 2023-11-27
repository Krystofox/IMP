import bpy
import bmesh
import mathutils
import os


if os.path.exists(bpy.path.abspath("//map.dump")):
    os.remove(bpy.path.abspath("//map.dump"))
dumpFile=open(bpy.path.abspath("//map.dump"),"w+")

mapCollection = bpy.data.collections["Map"]
props = mapCollection.children["Props"]

for prop in props.all_objects:
    name = prop.name.split(".")[0]
    line = 'PROP;'
    line += name + ';'
    for f in prop.location:
        line += str(f) + ';'
    for f in prop.rotation_euler:
        line += str(f) + ';'
    for f in prop.scale:
        line += str(f) + ';'
    
    line += '\n'
    dumpFile.write(line)
    

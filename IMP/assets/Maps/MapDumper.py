import bpy
import bmesh
import mathutils
import os

from bpy import context

import builtins as __builtin__

def console_print(*args, **kwargs):
    for a in context.screen.areas:
        if a.type == 'CONSOLE':
            c = {}
            c['area'] = a
            c['space_data'] = a.spaces.active
            c['region'] = a.regions[-1]
            c['window'] = context.window
            c['screen'] = context.screen
            s = " ".join([str(arg) for arg in args])
            for line in s.split("\n"):
                bpy.ops.console.scrollback_append(c, text=line)

def print(*args, **kwargs):
    """Console print() function."""

    console_print(*args, **kwargs) # to py consoles
    __builtin__.print(*args, **kwargs) # to system console

#print(bpy.data.scenes[0].objects["playermodel"])


mapCollection = bpy.data.collections["Map"]
if os.path.exists(bpy.path.abspath("//map.dump")):
    os.remove(bpy.path.abspath("//map.dump"))
dumpFile=open(bpy.path.abspath("//map.dump"),"w+")

for obj in mapCollection.all_objects:
    if obj.data is not None:
        match obj.data['type']:
            case 'Block':
                line = 'BLOCK;'
                line += obj.material_slots[0].name + ';'
                for f in obj.location:
                    line += str(f) + ';'
                for f in obj.rotation_euler:
                    line += str(f) + ';'
                line += str(len(obj.data.vertices)) + ';'
                for v in obj.data.vertices:
                    for f in v.co:
                        line += str(f) + ';'
                uv_layer = obj.data.uv_layers['UVMap']
                for triangle in obj.data.polygons:
                    vertices = list(triangle.vertices)
                    i = 0
                    for vertex in vertices:
                        uvCoord = uv_layer.data[triangle.loop_indices[i]].uv
                        line += str(uvCoord.x) + ';' + str(uvCoord.y) + ';'
                        i += 1
                
                line += '\n'
                #ADD UV DUMP
                dumpFile.write(line)
            case 'Clip':
                line = 'CLIP;'
                for f in obj.location:
                    line += str(f) + ';'
                for f in obj.rotation_euler:
                    line += str(f) + ';'
                line += str(len(obj.data.vertices)) + ';'
                for v in obj.data.vertices:
                    for f in v.co:
                        line += str(f) + ';'
                line += '\n'
                dumpFile.write(line)
    else:
        line = 'PROP;'
        line += bpy.data.collections[obj.name].library.name + ';'
        for f in obj.location:
            line += str(f) + ';'
        line += '\n'
        dumpFile.write(line)
        

dumpFile.close()


#####OLD
#mesh = bpy.data.meshes.new("hithoxExport")
#obj = bpy.data.objects.new(mesh.name, mesh)
#col = bpy.data.collections["export"]
#col.objects.link(obj)
#bpy.context.view_layer.objects.active = obj
#
#hitboxesCol = bpy.data.collections['hitboxes'].all_objects
#colLength = len(hitboxesCol)

#FOREACH
#for i in range(colLength):
#    cur = hitboxesCol[i]
#    
#    origin = cur.location
#    size = cur.data.vertices[6].co    
#    print(size)
#    if ( i == 0):
#        final =  ""
#    else:
#        final =  ";"
#    final += str(origin.x) + ";"
#    final += str(origin.y) + ";"
#    final += str(origin.z) + ";"
#    final += str(abs(size.x)) + ";"
#    final += str(abs(size.y)) + ";"
#    final += str(abs(size.z))
#    hitboxfile.write(final)
#hitboxfile.close()


#middle = cur.location
#right = cur.location + mathutils.Vector((0.0, cur.dimensions[2], 0.0))
#up = cur.location + mathutils.Vector((0.0, 0.0, cur.dimensions[1]))



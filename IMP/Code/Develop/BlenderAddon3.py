#bpy.context.selected_objects

import bpy
from bpy.props import IntProperty, FloatProperty
from bl_ui.generic_ui_list import draw_ui_list
from datetime import datetime

import os

        
class ObjManager(bpy.types.PropertyGroup):
    def dump():
        if os.path.exists(bpy.path.abspath("//map.dump")):
            os.remove(bpy.path.abspath("//map.dump"))
        dumpFile = open(bpy.path.abspath("//map.dump"),"w+")

        map = bpy.data.collections['Map']
        
#        dynamic = map.children["Dynamic"]
#        dynamicsT = ""
#        lenght = 0
#        for d in dynamic.all_objects:
#            line = ""
#            line += d['Object'] + ';'
#            line += ObjManager.getTransform(d)
#            line += '\n'
#            dynamicsT += line
#            lenght += 1
#        dumpFile.write(str(lenght)+"\n")
#        for l in dynamicsT:
#            dumpFile.write(l)
        
        static = map.children["Static"]
        staticsT = ""
        lenght = 0
        for s in static.all_objects:
            line = ""
            line += s['Model'] + ';'
            line += ObjManager.getTransform(s)
            line += '\n'
            staticsT += line
            lenght += 1
        dumpFile.write(str(lenght)+"\n")
        for l in staticsT:
            dumpFile.write(l)
        
        hitbox = map.children["Hitbox"]
        hitboxT = ""
        lenght = 0
        for h in hitbox.all_objects:
            line = ""
            line += ObjManager.getTransform(h)
            line += '\n'
            hitboxT += line
            lenght += 1
        dumpFile.write(str(lenght)+"\n")
        for l in hitboxT:
            dumpFile.write(l)

    def getTransform(obj):
            line = ""
            for f in obj.location:
                line += str(f) + ';'
            for f in obj.rotation_euler:
                line += str(f) + ';'
            for f in obj.scale:
                line += str(f) + ";"
            return line

    
    
class IMPButtonHandler(bpy.types.Operator):
    """ButtonHandler"""
    bl_idname = "imp.button_handler"
    bl_label = "ButtonHandler"
    run_func: bpy.props.StringProperty()

    def execute(self, context):
        match self.run_func:
            case "dump":
                ObjManager.dump()
        return {'FINISHED'}


class MyPanel(bpy.types.Panel):
    bl_label = "Imp connection"
    bl_idname = "imp.conn_ui"
    bl_space_type = 'VIEW_3D'
    bl_region_type = 'UI'
    bl_category = "Imp Develop"

    blender_objects = []

    def draw(self, context):
        layout = self.layout
        scene = context.scene

        layout.label(text=(datetime.now().strftime("%H:%M:%S")))
        op = layout.operator(IMPButtonHandler.bl_idname,text="DUMP")
        op.run_func = "dump"
        


classes = [
    ObjManager,
    IMPButtonHandler,
    MyPanel
]

class_register, class_unregister = bpy.utils.register_classes_factory(classes)


def register():
    class_register()


def unregister():
    class_unregister()


register()

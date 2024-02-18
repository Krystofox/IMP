#bpy.context.selected_objects

import bpy
from bpy.props import IntProperty, FloatProperty
from bl_ui.generic_ui_list import draw_ui_list
from datetime import datetime
import socket

class ConnectionCodeOperator(bpy.types.Operator):
    bl_idname = "imp.connection_code_operator"
    bl_label = "Server loop operator"
    instance = None
    _timer = None
    _cancel = False

    def modal(self, context, event):
        if self._cancel == True:
            self.cancel(context)
            self._cancel = False
            return {'FINISHED'}

        if event.type == 'TIMER':
            ConnectionCode.server_loop()
        return {'PASS_THROUGH'}
    
    def execute(self, context):
        try:
            if ConnectionCodeOperator.instance is not None:
                if ConnectionCodeOperator.instance._cancel is not True:
                    raise Exception("ERROR: SERVER LOOP NOT TERMINATED!")
        except ReferenceError:
            pass
        ConnectionCodeOperator.instance = self
        wm = context.window_manager
        self._timer = wm.event_timer_add(2, window=context.window)
        wm.modal_handler_add(self)
        return {'RUNNING_MODAL'}
    
    def cancel(self,context):
        print("CANCELING")
        wm = context.window_manager
        wm.event_timer_remove(self._timer)

    def running():
        return ConnectionCodeOperator._timer is not False
    
    def new_instance():
        bpy.ops.imp.connection_code_operator()

    def terminate():
        try:
            if ConnectionCodeOperator.instance is not None:
                ConnectionCodeOperator.instance._cancel = True
        except ReferenceError:
            pass
        

class ConnectionCode(bpy.types.PropertyGroup):
    status = "DISCONNECTED"
    _client = None

    def execute():
        ConnectionCodeOperator.terminate()
        print("INFO: Running connection code")
        if(ConnectionCode.connect() == False):
            print("ERROR: Could not connect")
            return
        ConnectionCodeOperator.new_instance()
        return
    
    def disconnect():
        print("WARNING: Connection stopped")
        ConnectionCode.status = "DISCONNECTED"
        ConnectionCodeOperator.terminate()

    def connect():
        try:
            ConnectionCode._client = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            ConnectionCode._client.settimeout(0.1)
            ConnectionCode._client.connect(("127.0.0.1",9228))
        except socket.error:
            ConnectionCode.status = "DISCONNECTED"
            return False
        ConnectionCode.status = "CONNECTED"
        return True
    
    def server_loop():
        client = ConnectionCode._client
        response = client.recv(4096)
        response = response.decode("utf-8")
        print("RESPONSE:" + response)

class ObjManager(bpy.types.PropertyGroup):
    bobjects = []

    def add():
        for o in bpy.context.selected_objects:
            if o not in ObjManager.bobjects:
                ObjManager.bobjects.append(o)

    def list():
        print("--------")
        for o in ObjManager.bobjects:
            print(o)
        print("--------")

    def refresh():
        for o in ObjManager.bobjects:
            try:
                o.type
            except ReferenceError:
                ObjManager.bobjects.remove(o)

    def clear():
        ObjManager.bobjects.clear()

    
    
class IMPButtonHandler(bpy.types.Operator):
    """ButtonHandler"""
    bl_idname = "imp.button_handler"
    bl_label = "ButtonHandler"
    run_func: bpy.props.StringProperty()

    def execute(self, context):
        match self.run_func:
            case "connect":
                ConnectionCode.execute()
            case "add":
                ObjManager.add()
            case "list":
                ObjManager.list()
            case "clear":
                ObjManager.clear()
            case "refresh":
                ObjManager.refresh()
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
        layout.label(text=(ConnectionCode.status))
        op = layout.operator(IMPButtonHandler.bl_idname,text="CONNECT")
        op.run_func = "connect"
        op = layout.operator(IMPButtonHandler.bl_idname,text="ADD")
        op.run_func = "add"
        op = layout.operator(IMPButtonHandler.bl_idname,text="LIST")
        op.run_func = "list"
        op = layout.operator(IMPButtonHandler.bl_idname,text="CLEAR")
        op.run_func = "clear"
        op = layout.operator(IMPButtonHandler.bl_idname,text="REFRESH")
        op.run_func = "refresh"
        
        


classes = [
    ConnectionCodeOperator,
    ConnectionCode,
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

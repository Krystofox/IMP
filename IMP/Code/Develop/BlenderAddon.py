import bpy
from bpy.props import IntProperty, FloatProperty
from bl_ui.generic_ui_list import draw_ui_list
from datetime import datetime
import socket

class ConnectionCode(bpy.types.Operator):
    """Connect to running instance of game"""
    bl_idname = "imp.connection_code"
    bl_label = "Connect"
    _timer = None
    _client = None

    def modal(self, context, event):
        if event.type in {'RIGHTMOUSE', 'ESC'}:
            self.cancel(context)
            return {'CANCELLED'}

        if event.type == 'TIMER':
            try:
                self.server_loop(context)
            except socket.error:
                self.cancel(context)

        return {'PASS_THROUGH'}
        
        #return {'PASS_THROUGH'}
        #return {'RUNNING_MODAL'}

    def execute(self, context):
        self.report({'INFO'}, "Running connection code")
        if(self.connect(context) == False):
            self.report({'ERROR'}, "Could not connect")
            return {'CANCELLED'}
        wm = context.window_manager
        self._timer = wm.event_timer_add(1, window=context.window)
        wm.modal_handler_add(self)
        return {'RUNNING_MODAL'}
    
    def cancel(self, context):
        self.report({'WARNING'}, "Connection stopped")
        wm = context.window_manager
        wm.event_timer_remove(self._timer)

    def connect(self,context):
        try:
            self._client = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            self._client.settimeout(5)
            self._client.connect(("127.0.0.1",9228))
        except socket.error:
            return False
        return True
    
    def server_loop(self,context):
        client = self._client
        response = client.recv(1024)
        response = response.decode("utf-8")
        self.report({'INFO'}, response)


class MyPropGroup(bpy.types.PropertyGroup):
    name: bpy.props.StringProperty()


class MyPanel(bpy.types.Panel):
    bl_label = "Imp connection"
    bl_idname = "imp.conn_ui"
    bl_space_type = 'VIEW_3D'
    bl_region_type = 'UI'
    bl_category = "Imp Develop"

    def draw(self, context):
        layout = self.layout
        scene = context.scene

        #draw_ui_list(
        #    layout,
        #    context,
        #    list_path="scene.my_list",
        #    active_index_path="scene.my_list_active_index",
        #    unique_id="my_list_id",
        #)
        layout.label(text=("Big Button:" + datetime.now().strftime("%H:%M:%S")))
        layout.label(text=("connection status"))
        row = layout.row()
        row.scale_y = 1.0
        row.operator("imp.connection_code")


classes = [
    MyPropGroup,
    MyPanel,
    ConnectionCode
]

class_register, class_unregister = bpy.utils.register_classes_factory(classes)


def register():
    class_register()
    bpy.types.Scene.my_list = bpy.props.CollectionProperty(type=MyPropGroup)
    bpy.types.Scene.my_list_active_index = bpy.props.IntProperty()


def unregister():
    class_unregister()
    del bpy.types.Scene.my_list
    del bpy.types.Scene.my_list_active_index


register()

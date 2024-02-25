#version 330

// Input vertex attributes
in vec3 vertexPosition;
in vec2 vertexTexCoord;
in vec3 vertexNormal;
in vec4 vertexColor;

// Input uniform values
uniform mat4 mvp;
uniform mat4 matModel;
uniform mat4 matNormal;

// Output vertex attributes (to fragment shader)
out vec3 fragPosition;
out vec2 fragTexCoord;
out vec4 fragColor;
out vec3 fragNormal;



void main()
{
    // Send vertex attributes to fragment shader
    //mvp - viewMatrix
    //matModel - wolrdspace
    //object space to worldspace
    fragPosition = vec3(matModel*vec4(vertexPosition, 1.0));
    fragTexCoord = vertexTexCoord;
    fragColor = vertexColor;
    fragNormal = normalize(vec3(matNormal*vec4(vertexNormal, 1.0)));
    
    
    //vec4 finalMatrix = mvp * vec4(finalPos,1.0);
    //finalMatrix = finalMatrix;
    //vertexPos2 = vertexPos2 * vertexTexCoord;
    //vec3 vertexPos2 = vertexPosition + vec3(fragTexCoord.y,fragTexCoord.x,0.0)*1.0;
    //vec3 vertexPos2 = vertexPosition + vec3(0.0,5.0,0.0);
    //gl_Position = mvp*vec4(vertexPos2.x,vertexPos2.y,0.0, 1.0);

    //UV remmap -1 1

    //vec3 offsetUV = vec3(fragTexCoord.y,fragTexCoord.x,0.0);
    //vec3 offsetUV = vec3(map(fragTexCoord.y,0,1,-1,1),map(fragTexCoord.x,0,1,-1,1),0.0);

    //gl_Position = mvp*vec4(fragPosition, 1.0) + mvp*vec4(offsetUV,1.0);
    //gl_Position = modelView*vec4(fragPosition, 1.0)*mvp;
    //float delta = (deltaT + matModel[0][0] + matModel[0][0] + matModel[0][0]) * fragPosition.z;
    //mat4 rotationMatrix = rotateMat(vec3(0.0,0.0,delta));
    //gl_Position = mvp*rotationMatrix*vec4(fragPosition, 1.0);
    
    gl_Position = mvp*vec4(fragPosition, 1.0);
}


FROM busybox
WORKDIR /build-context
COPY . .
RUN find .

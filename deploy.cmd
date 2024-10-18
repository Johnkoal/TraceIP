@echo off
docker build -f TraceIP.Api\Dockerfile -t traceip.api:latest .
docker build -f TraceIP.Web\Dockerfile -t traceip.web:latest .